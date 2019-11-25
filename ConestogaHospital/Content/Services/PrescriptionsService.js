app.service("PrescriptionsService", function ($http) {

    this.getPatientNoForAutoComplete = function (id) {
        var response = $http({
            method: "post",
            url: "/Reception/GetPatientNoForAutoComplete",
        });
        return response;
    }
    
    this.getDiagnosisList = function (patientNo) {
        var response = $http({
            method: "post",
            url: "GetDiagnosisesByPatientNo",
            params: {
                patientNo: patientNo
            }
        });
        return response;
    }
    this.viewPres = function (diagnosisId) {
        var response = $http({
            method: "post",
            url: "GetPresSymByDiagnosisId",
            params: {
                diagnosisId: diagnosisId
            }
        });
        return response;
    }

});