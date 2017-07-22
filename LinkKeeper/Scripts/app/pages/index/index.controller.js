(function (angular) {
    angular.module("linkKeeperModule")
        .controller("indexController", indexController);    
    indexController.$inject = ['$scope','$cookies','indexService']
    function indexController($scope, $cookies, indexService) {        
        this.logout = logout;        

        function logout() {
            indexService.logout($cookies.get('access_token')).then(function (response) {
                $cookies.remove('access_token');
                location.href = '/';
            }, function (response) {
                console.log(response);
            });
            
        }
    }
})(angular);