import angular from 'angular'

export default angular
    .module('uploadApp', [])
    .factory('uploadService', uploadService)
    .controller('UploadController', uploadController);

    uploadController.$inject = ['uploadService'];
    uploadService.$inject = ['$http', '$q','$window'];
	
function uploadController(uploadService) {
    var vm = this;
};

function uploadService($http, $q, $window) {
	return {
		
	};
}