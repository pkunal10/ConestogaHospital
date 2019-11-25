app.controller("TokenListCntrl", function ($scope, growl, TokenListService) {

    $("#divLoading").hide();
    getTokenList();
    

    function getTokenList() {
        $("#divLoading").show();
        TokenListService.getTokenList().then(function (data) {
            $("#divLoading").hide();
            if (data.data.Status == "Success") {
                $scope.tokenList = data.data.data;
            }
            else {
                $scope.tokenList = "";
                growl.error(data.data.msg, { title: 'Error!', ttl: 3000 });
            }

        })

    }
    
    $scope.deleteToken = function (tokenId) {
        $("#divLoading").show();
        TokenListService.deleteToken(tokenId).then(function (data) {
            if (data.data.Status == "Success") {
                $("#divLoading").hide();
                growl.success("Token is deleted.", { title: 'Success', ttl: 3000 });
                getTokenList();
            }            
        })
    }
    
});