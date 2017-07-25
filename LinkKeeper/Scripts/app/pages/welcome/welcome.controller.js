(function (angular) {
    angular
        .module("linkKeeperModule")        
        .controller("welcomeController", welcomeController);    
    welcomeController.$inject = ['$scope','$cookies']
    function welcomeController($scope,$cookies) {        
        this.getStarted = getStarted;

        function getStarted() {            
            if ($cookies.get('access_token')) {
                location.href = '/#!/links';
            } else {
                location.href = '/#!/login';
            }
        }
    }
})(angular);