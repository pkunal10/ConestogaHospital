app.controller("HomeScreenCntrl", function ($scope, growl, HomeScreenService) {

    getUserDetails();

    function getUserDetails() {
        HomeScreenService.getUserDetails().then(function (data) {
            if(data.data.Status=="Success")
            {
                $scope.userImage = data.data.data.image;
                $scope.userName = data.data.data.user.FName + " " + data.data.data.user.LName;
                $scope.userRole = data.data.data.role;
                $scope.speciality = data.data.data.speciality;
            }
            else {
                growl.error(data.data.msg, { title: 'Error!', ttl: 2000 });
            }
        });
    }

});