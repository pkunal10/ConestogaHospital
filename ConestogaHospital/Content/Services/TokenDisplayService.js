app.service("TokenDisplayService", function ($http) {

    this.tokenDisplay = function () {
        var response = $http({
            method: "post",
            url: "GetTokenList"
        });
        return response;
    }

    this.getUnAnnouncedTokens = function () {
        var response = $http({
            method: "post",
            url: "GetTokenListNotAnnounced"
        });
        return response;
    }

    this.updateTokenToAnnounced = function (id) {
        var response = $http({
            method: "post",
            url: "UpdateTokenToAnnounced",
            params: {
                tokenId: id
            }
        });
        return response;
    }

    this.getTokenToAnnounce = function () {
        var response = $http({
            method: "post",
            url: "GetTokenToAnnounce",            
        });
        return response;
    }

    this.removeTokenToAnnounce= function () {
        var response = $http({
            method: "post",
            url: "RemoveTokenToAnnounce",
        });
        return response;
    }

});