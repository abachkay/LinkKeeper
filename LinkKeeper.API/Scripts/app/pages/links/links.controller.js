(function (angular) {    
    angular.module("linkKeeperModule")
        .controller("linksController", linksController);
    linksController.$inject = ['$scope','linksService','$cookies'];
    function linksController($scope, linksService, $cookies) {   
        var vm = this;               
        vm.links = [];    
        vm.openModal = openModal;
        vm.createLink = createLink;
        vm.linkUrl = '';
        vm.linkName = '';
        vm.linkCategory = '';
        vm.linkErrors = [];
        vm.createLink = createLink;
        vm.deleteLink = deleteLink;
        vm.createOrUpdateLink = createOrUpdateLink;
        vm.updatingLinkId = 0;        
        vm.filterText = '';
        vm.getLinks = getLinks;
        vm.categories = [];
        getLinks();
        getCategories();
        $('.modal').modal(); 
         
        function getLinks(filterText) {      
            $('#loader').show();
            var url = 'api/Links';
            if (!filterText) {
                filterText = vm.filterText;
            }
            if (filterText != '') {
                url = 'api/Links/filter/' + filterText;
            }
            linksService.getLinks(url, $cookies.get('access_token')).then(function (response) {
                $('#loader').hide();
                vm.links = response.data;           
            }, function (response) {
                console.log(response);
                $('#loader').hide();
            });
        }

        function getCategories() {
            $('#loader').show();
            linksService.getCategories($cookies.get('access_token')).then(function (response) {                              
                var obj = {};
                var arr = response.data;
                for (var i = 0; i < arr.length; i++) {
                    obj[arr[i]] = null;
                }                
                $('.filterAutocomplete').autocomplete({
                    data: obj,
                    limit: 4,
                    onAutocomplete: function (val) {
                        getLinks(val);
                    },
                    minLength: 0,
                });                                                               
                $('#loader').hide();
            }, function (response) {
                console.log(response);                               
                $('#loader').hide();
            });
        }

        function openModal(url, name, category) {
            if (url && name && category) {
                vm.linkUrl = url;
                vm.linkName = name;
                vm.linkCategory = category;
            } else {
                vm.linkUrl = '';
                vm.linkName = '';
                vm.linkCategory = '';
            }
            Materialize.updateTextFields();                 
            $('#modalForLink').modal('open');
        }

        function createLink() {            
            linksService.createLink(vm.linkUrl, vm.linkName, vm.linkCategory, $cookies.get('access_token')).then(function (response) {
                getLinks();
                getCategories();
                $('#modalForLink').modal('close');
            }, function (response) {                           
                vm.linkErrors = [];
                for (var key in response.data['ModelState']) {
                    if (key != '$id') {
                        var arr = response.data['ModelState'][key];
                        for (var i = 0; i < arr.length; i++) {
                            vm.linkErrors.push(arr[i]);
                        }
                    }
                }                
            });
        }

        function deleteLink(id) {
            linksService.deleteLink(id, $cookies.get('access_token')).then(function (response) {
                getLinks();   
                getCategories();
            }, function (response) {
                console.log(response);
            });
        }

        function createOrUpdateLink() {    
            var promise;
            if (vm.updatingLinkId == 0) {
                promise = linksService.createLink(vm.linkUrl, vm.linkName, vm.linkCategory, $cookies.get('access_token'));
            } else {
                promise = linksService.updateLink(vm.updatingLinkId, vm.linkUrl, vm.linkName, vm.linkCategory, $cookies.get('access_token'));
            }
            promise.then(function (response) {
                getLinks();
                getCategories();
                $('#modalForLink').modal('close');
            }, function (response) {
                vm.linkErrors = [];
                for (var key in response.data['ModelState']) {
                    if (key != '$id') {
                        var arr = response.data['ModelState'][key];
                        for (var i = 0; i < arr.length; i++) {
                            vm.linkErrors.push(arr[i]);
                        }
                    }
                }
            });
            vm.updatingLinkId = 0;
        }
    }
})(angular);