(function (angular) {    
    angular
        .module("linkKeeperModule")        
        .controller("loginController", loginController);
    loginController.$inject = ['$scope','loginService','$cookies'];
    function loginController($scope, loginService, $cookies) {
        var vm = this;
        vm.email = '';
        vm.password = '';        
        vm.errors = '';
        vm.login = login;

        function login() {
            loginService.login(vm.email, vm.password).then(function (response) {                
                $cookies.put('access_token', response.data.access_token);
                $scope.$emit('loginEvent');
                //location.href = '/';
            }, function (response) {
                vm.errors = 'Invalid email or password.';
            });          
        }
    }
})(angular);