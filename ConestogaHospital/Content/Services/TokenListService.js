app.service("TokenListService", function ($http) {

    this.getTokenList = function () {
        var response = $http({
            method: "post",
            url: "GetTodaysNewToken"
        });
        return response;
    }
    this.deleteToken = function (id) {
        var response = $http({
            method: "post",
            url: "DeleteToken",
            params: {
                tokenId: id
            }
        });
        return response;
    }    
});