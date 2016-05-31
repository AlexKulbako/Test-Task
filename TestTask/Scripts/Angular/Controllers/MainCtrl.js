MainModule.controller('MainCtrl', function ($scope, FileSysService) {
    $scope.isUpper = true;
    FileSysService.GetDrives(function (result) {
        $scope.drives = result.drives;
    });
    $scope.GetData = function (path) {
        $scope.isUpper = false;
        FileSysService.GetSubDirs(path, function (result) {
            if (result.directories !== null) {
                $scope.dirs = result.directories;
                $scope.parent = result.directories[0].Parent;
            }
            else
                $scope.dirs = null;
        });
        FileSysService.GetSubFiles(path, function (result) {
            if (result.files !== null) {
                $scope.files = result.files;
                $scope.count = result.fileCount;
            } else {
                $scope.files = null;
                $scope.count = {
                    Less: 0,
                    Middle: 0,
                    More: 0
                };
            }

        });


    }
    $scope.IsUp = function (path) {
        if ($scope.parent === null)
            $scope.isUpper = true;
        else
            $scope.GetData(path);
    }
});