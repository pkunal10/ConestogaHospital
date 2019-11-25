app.service("TokenGenerateService", function ($http) {

    this.getSpecialityList = function () {
        var response = $http({
            method: "post",
            url: "/Admin/GetSpecialityList"
        });
        return response;
    }
    this.getPatientNo = function () {
        var response = $http({
            method: "post",
            url: "GetPatientNo"
        });
        return response;
    }
    this.getDoctorList = function (id) {
        var response = $http({
            method: "post",
            url: "GetDoctorListBySpeciality",
            params: {
                specialityId: id
            }
        });
        return response;
    }
    this.getPatientNoForAutoComplete = function (id) {
        var response = $http({
            method: "post",
            url: "GetPatientNoForAutoComplete",            
        });
        return response;
    }

    this.getTokenNo = function (id) {
        var response = $http({
            method: "post",
            url: "GetTokenNo",
            params: {
                specialityId: id
            }
        });
        return response;
    }

    this.saveTokenNo = function (modal) {
        var response = $http({
            method: "post",
            url: "SaveToken",
            data: modal,
            dataType: JSON.stringify
        });
        return response;
    }
    this.patientRegister = function (modal) {
        var response = $http({
            method: "post",
            url: "PatientRegister",
            data: modal,
            dataType: JSON.stringify
        });
        return response;
    }
});