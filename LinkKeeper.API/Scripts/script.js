$('#btnGetStarted').click(function () {
    location.href = '/Home/Links';
});

$('#submitLogin').click(function (e) {    
    e.preventDefault();
    var loginData = {
        grant_type: 'password',
        username: $('#emailLogin').val(),
        password: $('#passwordLogin').val(),       
    };
    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData,
    }).done(function (data) {
        sessionStorage.setItem('token', data.access_token);
        document.cookie = data.access_token;
        location.href = '/';
    }).fail(function (data) {        
        $('#errorTextLogin').html('User email or password is incorrect.');
    });
}); 

$('#submitRegister').click(function (e) {
    e.preventDefault();
    var data = {
        Email: $('#emailRegister').val(),
        Password: $('#passwordRegister').val(),
        ConfirmPassword: $('#passwordConfirmRegister').val()
    };
    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),       
    }).done(function (data) {
        location.href = '/Home/Login';
    }).fail(function (data) {
        var errorText = "";
        var response = JSON.parse(data['responseText']);
        for (var key in response['ModelState']) {
            var arr = response['ModelState'][key];
            for (var i = 0; i < arr.length; i++) {
                errorText += arr[i] + '<br/>';
            }
        }
        $('#errorTextRegister').html(errorText);
    });
});

function getLinks() {
    $('#loader').show();
    var category = $('#inputFilter').val();
    var url = '/api/Links';    
    if (category != '') {
        url += '/filter/' + category;
    }
    $.ajax({
        type: 'GET',
        url: url,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function (xhr) {
            var token = document.cookie;
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function (data) {
        //for (var i = 0; i < 2000000000; i++) { }
        var linksTableBody = $('#linksTableBody');
        linksTableBody.html('');       
        for (var i = 0; i < data.length; i++) {            
            var json = JSON.stringify(data[i]);
            var category = data[i]['Category'] || 'Uncategorized'; 
            linksTableBody.append('<tr><td><a href="' + data[i]['Url'] +'">' + data[i]['Url'] + '<a></td><td>' + data[i]['Name'] + '</td><td>' + category +
                "</td><td><button onclick='openModalToUpdate(" + json + ")' class='btn btn-floating amber accent-4'><i class='material-icons'>edit</i></button></td>" +
                '<td><button onclick="deleteLink(' + data[i]['LinkId'] +')" class="btn btn-floating amber accent-4"><i class="material-icons">delete</i></button></td></tr>');
        }
        $('#loader').hide();
    }).fail(function (data) {
        location.href = '~/Content/400.html';
    });      
}

function deleteLink(id)
{
    $.ajax({
        type: 'DELETE',
        url: '/api/Links/'+id,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function (xhr) {
            var token = document.cookie;
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function (data) {
        getLinks();
    }).fail(function (data) {
        location.href = '~/Content/400.html';
    }); 
}

$('#btnOpenLinkModal').click(function () {    
    $('#modalLink').openModal();
    $('#btnAddLink').removeAttr('link');

    $('#linkErrors').html('');
    $('#Url').val('');
    $('#Name').val('');
    $('#Category').val('');
    Materialize.updateTextFields();
});    

function openModalToUpdate(link) {    
    $('#modalLink').openModal();    
    $('#btnAddLink').attr('link', JSON.stringify(link));   

    $('#linkErrors').html('');
    $('#Url').val(link['Url']);
    $('#Name').val(link['Name']);
    $('#Category').val(link['Category']);
    Materialize.updateTextFields();
}

$('#btnAddLink').click(function () {    
    var link = $('#btnAddLink').attr('link');          
    var methodType = '';
    var url = '/api/Links/';
    if (typeof link !== 'undefined') {        
        methodType = 'PUT';
        link = JSON.parse(link);        
        url += ''+link["LinkId"];
        
    } else {
        methodType = 'POST';           
    }
        
    var linkModel = JSON.stringify({
        'Url': $('#Url').val(),
        'Name': $('#Name').val(),
        'Category': $('#Category').val(),
    });
    $.ajax({
        type: methodType,
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: linkModel,
        beforeSend: function (xhr) {
            var token = document.cookie;
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    }).done(function (data) {
        $('#modalLink').closeModal();
        getLinks();
    }).fail(function (data) {
        var errorText = "";
        var response = JSON.parse(data['responseText']);
        for (var key in response['ModelState']) {
            if (key != '$id') {
                var arr = response['ModelState'][key];
                for (var i = 0; i < arr.length; i++) {
                    errorText += arr[i] + '<br/>';
                }
            }
        }
        $('#linkErrors').html(errorText);
    });
});


$('#inputFilter').on('change keyup paste', function () {
    getLinks();  
});

