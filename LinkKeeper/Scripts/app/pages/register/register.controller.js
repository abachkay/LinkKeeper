(function (angular) {    
    angular
        .module("linkKeeperModule")        
        .controller("registerController", registerController);
    registerController.$inject = ['$scope','registerService'];
    function registerController($scope, registerService) {
        var vm = this;
        vm.email = '';
        vm.password = '';   
        vm.passwordConfirm = '';   
        vm.errors = [];
        vm.register = register;

        function register() { 
            registerService.register(vm.email, vm.password, vm.passwordConfirm).then(function (response) {
                location.href = '/#!/login';
            }, function (response) {                
                vm.errors = [];                                                             
                for (var key in response.data['ModelState']) {
                    var arr = response.data['ModelState'][key];
                    for (var i = 0; i < arr.length; i++) {
                        vm.errors.push(arr[i]);
                    }
                }
            });          
        }
    }
})(angular);