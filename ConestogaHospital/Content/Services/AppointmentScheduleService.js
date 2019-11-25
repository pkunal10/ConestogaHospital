app.service("AppointmentScheduleService", function ($http) {

    this.getDoctorDetails = function () {
        var response = $http({
            method: "post",
            url: "GetDoctorDetails"
        });
        return response;
    }
    this.getAppointments= function () {
        var response = $http({
            method: "post",
            url: "GetDoctorSchedule"
        });
        return response;
    }    
});