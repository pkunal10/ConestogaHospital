app.controller("TokenDisplayCntrl", function ($scope, growl, TokenDisplayService, Pubnub) {

    $("#divLoading").hide();
    tokenDisplay();

    function tokenDisplay() {

        TokenDisplayService.tokenDisplay().then(function (data) {

            if (data.data.Status == "Success") {
                $scope.newTokens = data.data.data.listNewToken;
                $scope.underDiagnosisTokens = data.data.data.listUnderDiagnosisToken;
            }

        })

    }

    //setTimeout(function () {
    //    speak("We call tokens as per doctor availability.")
    //}, 2000);

    setInterval(function () {
        announceTokens();
    }, 4000)


    function announceTokens() {
        TokenDisplayService.getUnAnnouncedTokens().then(function (data) {

            if(data.data.Status=="Success")
            {
                for(var i=0;i<data.data.data.length;i++)
                {
                    var token = data.data.data[i];
                    var text = "Token number " + token.TokenNo + " in room " + token.RoomNo + " to doctor " + token.DoctorName + ".";
                    speak(text);
                    TokenDisplayService.updateTokenToAnnounced(token.TokenId).then(function (data) {

                    })
                }
            }

        })
        TokenDisplayService.getTokenToAnnounce().then(function (data) {
            if (data.data.Status == "Success") {
                var token = data.data.data;
                var text = "Token number " + token.TokenNo + " in room " + token.RoomNo + " to doctor " + token.DoctorName + ".";
                speak(text);
                TokenDisplayService.removeTokenToAnnounce().then(function (data) {

                })
            }
        })
        tokenDisplay();
    }
    function speak(text) {
        window.speechSynthesis.speak(new SpeechSynthesisUtterance(text));
    }

});