(function (angular) {
    angular
        .module("linkKeeperModule")
        .controller("indexController", indexController);    
    indexController.$inject = ['$scope','$cookies','indexService']
    function indexController($scope, $cookies, indexService) {        
        var vm = this;
        vm.firstLinkText = 'Login';        
        vm.secondLinkText = 'Register';        
        vm.firstLinkClick = login;
        vm.secondLinkClick = register;        
        init();

        function init() {
            if ($cookies.get('access_token')) {
                vm.firstLinkText = 'Links';
                vm.secondLinkText = 'Logout';
                vm.firstLinkClick = links;
                vm.secondLinkClick = logout;       
            }
        }
        function login() {
            location.href = '/#!/login';
        }
        function register() {
            location.href = '/#!/register';
        }
        function links() {
            location.href = '/#!/links';
        }
        function logout() {
            $cookies.remove('access_token');
            vm.firstLinkText = 'Login';
            vm.secondLinkText = 'Register';
            vm.firstLinkClick = login;
            vm.secondLinkClick = register;
            location.href = '/#!/login';
            indexService.logout($cookies.get('access_token')).then(function (response) {                
            }, function (response) {
                console.log(response);
            });            
        }

        $scope.$on('loginEvent', function (event) {
            vm.firstLinkText = 'Links';
            vm.secondLinkText = 'Logout';
            vm.firstLinkClick = links;
            vm.secondLinkClick = logout;  
            location.href = '/#!/';
        });
    }
})(angular);