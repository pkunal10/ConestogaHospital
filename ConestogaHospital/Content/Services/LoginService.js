app.service("LoginService", function ($http) {


    this.login = function (uname, password) {

        var response = $http({
            method: 'post',
            url: '/Home/LoginCheck',
            params: {
                Uname: uname,
                Password: password
            }
        });
        return response;
    }

    this.getdoctorlist = function () {
        var response = $http({
            method: "post",
            url: "/Home/GetDoctorListForDropDown"
        });
        return response;
    }

    this.getAppointmentSlots = function (date,doctorId) {
        var response = $http({
            method: "post",
            url: "/Home/GetAppointmentSlots",
            params: {
                date: date,
                doctorId: doctorId
            }
        });
        return response;
    }

    this.getPatientNoForAutoComplete = function (id) {
        var response = $http({
            method: "post",
            url: "/Reception/GetPatientNoForAutoComplete",
        });
        return response;
    }

    this.saveAppointment = function (model) {
        var response = $http({
            method: "post",
            url: "/Home/SaveAppointment",
            data: model,
            dataType: JSON.stringify
        });
        return response;
    }

});