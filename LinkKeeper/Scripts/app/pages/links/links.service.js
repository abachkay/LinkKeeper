(function (angular) {
    angular
        .module("linkKeeperModule")
        .factory("linksService", linksService);
    linksService.$inject = ['$http'];
    function linksService($http) {
        return {
            getLinks: getLinks,
            createLink: createLink,
            deleteLink: deleteLink,
            updateLink: updateLink,
            getCategories: getCategories
        }       
        function getLinks(url,token) {
            return $http({
                method: 'GET',
                url: url ,   
                headers: {
                    'Authorization': 'Bearer '+ token
                }
            }); 
        }

        function getCategories(token) {
            return $http({
                method: 'GET',
                url: '/api/links/categories',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }); 
        }

        function createLink(url, name, category, isFavorite, token) {
            return $http({
                method: 'POST',
                url: '/api/links',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                data: {
                    'Url': url,
                    'Name': name,
                    'Category': category,
                    'IsFavorite': isFavorite
                }
            }); 
        }

        function deleteLink(id, token) {
            return $http({
                method: 'DELETE',
                url: '/api/links/'+id,
                headers: {
                    'Authorization': 'Bearer ' + token
                },               
            });
        }

        function updateLink(id, url, name, category, isFavorite, token) {
            return $http({
                method: 'PUT',
                url: '/api/links/'+id,
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                data: {
                    'Url': url,
                    'Name': name,
                    'Category': category,
                    'IsFavorite': isFavorite
                }
            }); 
        }
    }
})(angular);