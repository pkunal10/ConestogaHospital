app.service("DiagnosisService", function ($http) {


    this.getTokenDetails = function () {
        var response = $http({
            method: "post",
            url: "GetTokenDetailsByDoctorId"
        });
        return response;
    }

    this.fetchRoomNos = function () {
        var response = $http({
            method: "post",
            url: "FetchRoomNos"
        });
        return response;
    }

    this.speak = function (text) {

        var response = $http({
            method: 'post',
            url: 'speak',
            params: {
                text: text
            }
        });
        return response;
    }
    this.saveDiagnosis = function (model) {

        var response = $http({
            method: 'post',
            url: 'SaveDiagnosis',
            data: model,
            dataType: JSON.stringify
        });
        return response;
    }

    this.announceToken = function (id) {
        var response = $http({
            method: 'post',
            url: 'AnnounceToken',
            params: {
                tokenId: id
            }
        });
        return response;
    }

    this.getPatientAlergiesAndLastDiagnosis = function (id) {
        var response = $http({
            method: 'post',
            url: 'GetPatientLastDiagnosisAndAlergies',
            params: {
                patientId: id
            }
        });
        return response;
    }

    this.makeUnderDiagnosis = function (id) {

        var response = $http({
            method: 'post',
            url: 'MakeUnderDiagnosis',
            params: {
                tokenId:id
            }
        });
        return response;
    }

});