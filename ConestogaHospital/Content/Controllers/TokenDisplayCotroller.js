app.controller("LoginCntrl", function ($scope, growl, LoginService) {

    $("#divLoading").hide();
    $scope.selectedSlot = null;

    $scope.Login = function () {

        if ($scope.UserName == "" || $scope.UserName == undefined) {
            growl.error("Enter Username.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        else if ($scope.Password == "" || $scope.Password == undefined) {
            growl.error("Enter Password.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        else {
            $("#divLoading").show();
            LoginService.login($scope.UserName, $scope.Password).then(function (data) {

                if (data.data.Status == "AuthenticationFailed") {
                    growl.error(data.data.msg, { title: 'Error!', ttl: 2000 });
                    $scope.UserName = "";
                    $scope.Password = "";
                    $("#divLoading").hide();
                }
                else {
                    location.href = '/Home/HomeScreen';
                }


            });
        }

    }

    $scope.bookAppointment = function () {
        $('#appointmentModal').modal('show');
        LoginService.getdoctorlist().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.doctorList = data.data.data;
            }
        });
    }

    $scope.doctorChange = function () {
        if ($scope.DoctorId == undefined || $scope.DoctorId == 0) {
            growl.error('Select doctor first.', { title: 'Error!', ttl: 2000 });
            $scope.selectedSlot = null;
            $scope.slots = "";
            $scope.caption = "";
        }
        else {
            var month = $scope.AppointmentDate.getMonth();
            month++;
            var appointmentDate = $scope.AppointmentDate.getDate() + "/" + month + "/" + $scope.AppointmentDate.getFullYear();
            $("#divLoading").show();
            LoginService.getAppointmentSlots(appointmentDate, $scope.DoctorId).then(function (data) {
                if (data.data.Status == "Success") {
                    for (var i = 0; i < $scope.doctorList.length; i++) {
                        if ($scope.DoctorId == null) {
                            $scope.caption = "";
                            $scope.slots = "";
                        }
                        else if ($scope.doctorList[i].Value == $scope.DoctorId) {
                            $scope.caption = appointmentDate + ", Doctor: " + $scope.doctorList[i].Text;
                            break;
                        }
                        else {
                            $scope.caption = "";
                            $scope.slots = "";
                        }
                    }
                    $scope.slots = data.data.data;
                    $("#divLoading").hide();
                }
            })
        }
    }
    $scope.saveSlot = function (slot) {
        if (slot.IsAvailable == false) {
            growl.error('This slot is not available.', { title: 'Error!', ttl: 2000 });
        }
        else {
            $scope.selectedSlot = slot;
            for (var i = 0; i < $scope.slots.length; i++) {
                if ($scope.slots[i].IsAvailable == true)
                    $scope.slots[i].BackgroundColor = "#5cb85c";
            }
            slot.BackgroundColor = "#757575";
        }
    }

    $scope.SaveAppointment = function () {
        if ($scope.PatientNo == "" || $scope.PatientNo == undefined) {
            growl.error("Enter patient no.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.AppointmentDate == "" || $scope.AppointmentDate == undefined) {
            growl.error("Enter appointment date.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.DoctorId == 0 || $scope.DoctorId == undefined) {
            growl.error("Select doctor.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.selectedSlot == "" || $scope.selectedSlot == null) {
            growl.error("Select appointment time slot.", { title: 'Error!', ttl: 2000 });
            return false;
        }
        var month = $scope.AppointmentDate.getMonth();
        month++;
        var appointmentDate = $scope.AppointmentDate.getDate() + "/" + month + "/" + $scope.AppointmentDate.getFullYear();
        var appointment = {
            PatientNo: $scope.PatientNo,
            DoctorId: $scope.DoctorId,
            AppointmentDate: appointmentDate,
            SelectedSlot: $scope.selectedSlot
        }
        $("#divLoading").show();
        LoginService.saveAppointment(appointment).then(function (data) {
            if (data.data.Status == "Success") {
                $("#divLoading").hide();
                $('#appointmentModal').modal('hide');
                $scope.selectedSlot = null;
                $scope.PatientNo = "";
                $scope.AppointmentDate = "";
                $scope.DoctorId = 0;
                $scope.slots = "";
                $scope.caption = "";
                growl.success(data.data.msg, { title: 'Success!', ttl: 2000 });
            }
            else {
                $("#divLoading").hide();
                $scope.selectedSlot = null;
                growl.error(data.data.msg, { title: 'Error!', ttl: 2000 });
            }
        }), function () {
            $("#divLoading").hide();
            growl.error('Something went wrong.', { title: 'Error!', ttl: 2000 });
        }
    }
    $scope.clear = function () {
        $scope.selectedSlot = null;
        $scope.PatientNo = "";
        $scope.AppointmentDate = "";
        $scope.DoctorId = 0;
        $scope.slots = "";
        $scope.caption = "";
    }
});