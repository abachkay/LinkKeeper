(function (angular) {
    angular.module("linkKeeperModule")
        .controller("welcomeController", welcomeController);

    //linkKeeperController.$inject = ['$scope', "tasksService"];
    function welcomeController($scope/*, tasksService*/) {        
       this.hello = "Hello";        
    }
})(angular);