(function (angular) {
    angular
        .module("linkKeeperModule")
        .factory("loginService", loginService);
    loginService.$inject = ['$http'];
    function loginService($http) {
        return {
            login: login
        }       
        function login(email,password) {
            return $http({
                method: 'POST',
                url: '/Token',
                headers: { 'Content-Type': 'x-www-form-urlencoded' },
                data: 'grant_type=password&username=' + email + '&password=' + password
            }); 
        }
    }
})(angular);