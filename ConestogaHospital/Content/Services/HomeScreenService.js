app.service("HomeScreenService", function ($http) {


    this.getUserDetails = function () {
        var response = $http({
            method: "post",
            url: "GetUserDetailsById"
        });
        return response;
    }
   
});