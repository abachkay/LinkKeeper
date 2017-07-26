(function (angular) {
    angular
        .module("linkKeeperModule")
        .config(function ($stateProvider, $locationProvider) {            
            $stateProvider.state('welcome', {
                url: '/',
                templateUrl: '/Content/pages/welcome.html',
                controller: 'welcomeController',
                controllerAs: 'vm'
            }).state('links', {
                url: '/links',
                templateUrl: '/Content/pages/links.html',
                controller: 'linksController',
                controllerAs: 'vm'
            }).state('login', {
                url: '/login',
                templateUrl: '/Content/pages/login.html',
                controller: 'loginController',
                controllerAs: 'vm'
            }).state('register', {
                url: '/register',
                templateUrl: '/Content/pages/register.html',
                controller: 'registerController',
                controllerAs: 'vm'
            });          
            //$locationProvider.hashPrefix('!');
            //$locationProvider.html5Mode(true);
    });   
})(angular);