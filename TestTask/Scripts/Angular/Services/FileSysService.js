MainModule.service("FileSysService", function ($http) {

    this.GetDrives = function (callback) {
        $http.get('/api/filesys/getDrives').success(function (result) {
            callback(result);
        });
    }
    this.GetSubDirs = function (path, callback) {
        $http.get('/api/filesys/getSubDirectoriesByPath?path=' + path).success(function (result) {
            callback(result);
        });
    }
    this.GetSubFiles = function (path, callback) {
        $http.get('/api/filesys/getFilesByPath?path=' + path).success(function (result) {
            callback(result);
        });
    }

});