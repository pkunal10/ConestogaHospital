app.service("AdmitPatientListService", function ($http) {

    this.getAdmitPatientList = function () {
        var response = $http({
            method: "post",
            url: "GetAdmitPatientList"
        });
        return response;
    }
    this.dischargePatient = function (id) {
        var response = $http({
            method: "post",
            url: "DischargePatient",
            params: {
                detailsId: id
            }
        });
        return response;
    }    
});