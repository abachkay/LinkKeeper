(function (angular) {
    angular.module("linkKeeperModule")
        .factory("registerService", registerService);
    registerService.$inject = ['$http'];
    function registerService($http) {
        return {
            register: register
        }       
        function register(email,password,passwordConfirm) {
            return $http({
                method: 'POST',
                url: '/api/Account/Register',                
                data: {
                    Email: email,
                    Password: password,
                    ConfirmPassword: passwordConfirm
                }
            }); 
        }
    }
})(angular);