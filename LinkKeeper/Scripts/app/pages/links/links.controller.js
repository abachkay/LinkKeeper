(function (angular) {    
    angular
        .module("linkKeeperModule")
        .controller("indexController")
        .controller("linksController", linksController);
    linksController.$inject = ['$scope','linksService','$cookies'];
    function linksController($scope, linksService, $cookies) {   
        var vm = this;               
        vm.links = [];    
        vm.openModal = openModal;        
        vm.linkUrl = '';
        vm.linkName = '';
        vm.linkCategory = '';
        vm.linkIsFavorite = false;
        vm.linkErrors = [];        
        vm.deleteLink = deleteLink;
        vm.createOrUpdateLink = createOrUpdateLink;
        vm.updatingLinkId = 0;        
        vm.filterText = '';
        vm.getLinks = getLinks;
        vm.categories = [];
        vm.onlyFavorite = false;
        vm.getLinks = getLinks;
        getLinks();
        getCategories();
        $('.modal').modal();                  
        function getLinks(filterText) {      
            $('#loader').show();
            var url = 'api/Links';
            if (!filterText) {
                filterText = vm.filterText;
            } else {
                vm.filterText = filterText;
            }
            if (filterText != '') {
                url = 'api/Links/filter/' + filterText;
            }
            linksService.getLinks(url, $cookies.get('access_token')).then(function (response) {
                $('#loader').hide();
                var links = response.data;                
                if (vm.onlyFavorite) {
                    links = links.filter(function (element) {                        
                        return element.IsFavorite == true;                        
                    }); 
                } 
                vm.links = links;
            }, function (response) {                
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
                $('#loader').hide();
            });
        }

        function openModal(url, name, category, isFavorite) {
            if (url && name && category && typeof(isFavorite) != 'undefined') {
                vm.linkUrl = url;
                vm.linkName = name;
                vm.linkCategory = category;
                vm.linkIsFavorite = isFavorite;
            } else {
                vm.linkUrl = '';
                vm.linkName = '';
                vm.linkCategory = '';
                vm.linkIsFavorite = false;
            }
            Materialize.updateTextFields();                 
            $('#modalForLink').modal('open');
        }

        function deleteLink(id) {
            linksService.deleteLink(id, $cookies.get('access_token')).then(function (response) {
                getLinks();   
                getCategories();
            }, function (response) {                
            });
        }

        function createOrUpdateLink() {    
            var promise;
            if (vm.updatingLinkId == 0) {
                promise = linksService.createLink(vm.linkUrl, vm.linkName, vm.linkCategory, vm.linkIsFavorite, $cookies.get('access_token'));
            } else {
                promise = linksService.updateLink(vm.updatingLinkId, vm.linkUrl, vm.linkName, vm.linkCategory, vm.linkIsFavorite, $cookies.get('access_token'));
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