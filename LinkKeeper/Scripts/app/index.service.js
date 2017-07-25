(function (angular) {
    angular
        .module("linkKeeperModule")
        .factory("indexService", indexService);
    indexService.$inject = ['$http'];
    function indexService($http) {
        return {
            logout: logout
        }       
        function logout(token) {
            return $http({
                method: 'POST',
                url: '/api/Account/Logout' ,   
                headers: {
                    'Authorization': 'Bearer '+ token
                }
            }); 
        }
    }
})(angular);