app.controller("AppointmentScheduleCntrl", function ($scope, growl, AppointmentScheduleService) {

    $("#divLoading").hide();
    getDoctorDetails();
    getAppointments();
    $scope.Doctor = null;


    function getDoctorDetails() {
        $("#divLoading").show();
        AppointmentScheduleService.getDoctorDetails().then(function (data) {
            $("#divLoading").hide();
            if (data.data.Status == "Success") {
                $scope.Doctor = data.data.data;
                $scope.today = data.data.today;
            }
            else {

            }

        })

    }
    function getAppointments() {
        $("#divLoading").show();
        AppointmentScheduleService.getAppointments().then(function (data) {
            $("#divLoading").hide();
            if (data.data.Status == "Success") {
                $scope.appointmentList = data.data.data;                
            }
            else {
                growl.error(data.data.msg, { title: 'Error!', ttl: 3000 });
            }

        })

    }    
});