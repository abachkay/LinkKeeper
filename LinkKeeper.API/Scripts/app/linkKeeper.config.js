(function (angular) {
    angular.module("linkKeeperModule")
        .config(function ($routeProvider, $locationProvider) {            
            $routeProvider            
                .when('/', {
                    templateUrl: '/Content/pages/Welcome.html',
                    controller: 'welcomeController as vm'
                })
                .when('/login', {
                    templateUrl: '/Content/pages/Login.html',
                    controller: 'welcomeController as vm'
                    })                      
                .when('/register', {
                    templateUrl: '/Content/pages/Register.html',
                    controller: 'welcomeController as vm'
                })  
                .when('/links', {
                    templateUrl: '/Content/pages/Links.html',
                    controller: 'welcomeController as vm'
                })  
            $locationProvider.html5Mode(true);
    });   
})(angular);