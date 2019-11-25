app.controller("TokenGenerateCntrl", function ($scope, growl, TokenGenerateService) {

    $("#divLoading").hide();
    $scope.isNewPatient = false;
    $scope.Alergies = [];
    

    function getSpecialityList() {

        TokenGenerateService.getSpecialityList().then(function (data) {

            if (data.data.Status == "Success") {
                $scope.specialityList = data.data.data;
            }

        })

    }

    function getPatientNoForAutoComplete() {

        TokenGenerateService.getPatientNoForAutoComplete().then(function (data) {

            if (data.data.Status == "Success") {
                $scope.patientNoList = data.data.data;
            }

        })

    }
    function getPatientNo() {

        TokenGenerateService.getPatientNo().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.newPatientPatientNo = data.data.data;
            }
        })

    }
    $scope.openGenerateTokenModal = function () {
        $("#generateTokenModal").modal('show');
        getSpecialityList();
        getPatientNoForAutoComplete();
    }

    $scope.specialityChange = function () {
        if ($scope.SpecialityId == "" || $scope.SpecialityId == undefined) {
            growl.error("Select speciality first.", { title: 'Error!', ttl: 3000 });
            $scope.doctorList = "";
            $scope.DoctorId = "";
        }
        else {
            TokenGenerateService.getDoctorList($scope.SpecialityId).then(function (data) {
                if (data.data.Status == "Success") {
                    $scope.doctorList = data.data.data;
                }
                else {
                    growl.error("No Doctor available for this speciality.", { title: 'Error!', ttl: 3000 });
                    $scope.clear();
                }
            })
            TokenGenerateService.getTokenNo($scope.SpecialityId).then(function (data) {

                if (data.data.Status == "Success") {
                    $scope.tokenNo = data.data.data;
                }
            })
        }
    }
    $scope.clear = function () {
        $scope.doctorList = "";
        $scope.DoctorId = "";
        $scope.PatientNo = "";
        $scope.tokenNo = "";
        $scope.SpecialityId = "";
    }

    $scope.saveTokenNo = function () {
        if ($scope.PatientNo == "" || $scope.PatientNo == undefined) {
            growl.error("Enter patient no.", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.SpecialityId == "" || $scope.SpecialityId == undefined) {
            growl.error("Select speciality first.", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.DoctorId == "" || $scope.DoctorId == undefined) {
            growl.error("Select doctor first.", { title: 'Error!', ttl: 3000 });
            return false;
        }
        $("#divLoading").show();
        var modal = {
            PatientNo: $scope.PatientNo,
            DoctorId: $scope.DoctorId,
            TokenNo: $scope.tokenNo
        }
        TokenGenerateService.saveTokenNo(modal).then(function (data) {
            if (data.data.Status == "Success") {
                growl.success("Token no " + $scope.tokenNo + " is generated", { title: 'Success', ttl: 3000 });
                $("#divLoading").hide();
                $("#generateTokenModal").modal('hide');
                $scope.clear();
            }
            else if (data.data.Status == "No Patient") {
                growl.error("Invalid patient no.", { title: 'Error!', ttl: 3000 });
                $scope.PatientNo = "";
                $("#divLoading").hide();
            }
        })
    }

    $scope.complete = function (string) {

        var output = [];

        if (string == "") {
            $scope.filterPatientNo = null;
        }
        else {
            angular.forEach($scope.patientNoList, function (patientNo) {
                if (patientNo.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                    output.push(patientNo);
                }
            });
            $scope.filterPatientNo = output;
        }

    }
    $scope.fillTextbox = function (string) {
        $scope.PatientNo = string;
        $scope.filterPatientNo = null;
    }

    $scope.newPatientClick = function () {
        $scope.isNewPatient = true;
        getPatientNo();
    }
    $scope.addAllergy = function () {
        $scope.Alergies.push({ 'AlergyName': '' });
    }
    $scope.removeAllergy = function (allergy) {
        var index = $scope.Alergies.indexOf(allergy);
        $scope.Alergies.splice(index, 1);
    };

    $scope.patientRegister = function () {

        if ($scope.newPatientPatientNo == "" || $scope.newPatientPatientNo == undefined) {
            growl.error("Patient no required", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.FName == "" || $scope.FName == undefined) {
            growl.error("Enter first name", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.LName == "" || $scope.LName == undefined) {
            growl.error("Enter last name", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.MobileNo == "" || $scope.MobileNo == undefined) {
            growl.error("Enter mobile", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.EmailId== "" || $scope.EmailId == undefined) {
            growl.error("Enter email id", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.DOB == "" || $scope.DOB == undefined) {
            growl.error("Select DOB", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.Line1 == "" || $scope.Line1 == undefined) {
            growl.error("Enter Line 1 ", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.City == "" || $scope.City == undefined) {
            growl.error("Enter city ", { title: 'Error!', ttl: 3000 });
            return false;
        }

        if ($scope.Province == "" || $scope.Province == undefined) {
            growl.error("Enter Province", { title: 'Error!', ttl: 3000 });
            return false;
        }
        if ($scope.ZipCode == "" || $scope.ZipCode == undefined) {
            growl.error("Enter zip code", { title: 'Error!', ttl: 3000 });
            return false;
        }
        for (var i = 0; i < $scope.Alergies.length; i++) {
            if ($scope.Alergies[i].AlergyName == "" || $scope.Alergies[i].AlergyName == undefined) {
                var pos = i + 1;
                growl.error("Enter allergy at " + pos + " position.", { title: 'Error!', ttl: 3000 });
                return false;
            }
        }

        var month = $scope.DOB.getMonth();
        month++;
        var dob = $scope.DOB.getDate() + "/" + month + "/" + $scope.DOB.getFullYear();
        var address = {
            Line1: $scope.Line1,
            City: $scope.City,
            Province: $scope.Province,
            ZipCode: $scope.ZipCode
        }
        var patient = {
            FName: $scope.FName,
            LName: $scope.LName,
            PatientNo: $scope.newPatientPatientNo,
            DOB: dob,
            MobileNo: $scope.MobileNo,
            EmailId:$scope.EmailId,
        }
        var modal = {
            Patient: patient,
            Alergies: $scope.Alergies,
            Address: address
        }
        $("#divLoading").show();
        TokenGenerateService.patientRegister(modal).then(function (data) {
            if (data.data.Status == "Error") {
                $("#divLoading").hide();               
                growl.error(data.data.msg, { title: 'Error!', ttl: 2000 });
            }
            else if (data.data.Status == "Success") {
                $("#divLoading").hide();
                growl.success(data.data.msg, { title: 'Success', ttl: 2000 });
                $scope.clearNewPatient();
            }


        }, function () {
            $("#divLoading").hide();
            growl.error("Data is not saved", { title: 'Error!', ttl: 2000 });
        });
    }
    $scope.clearNewPatient = function () {
        $scope.Line1 = "";
        $scope.City = "";
        $scope.Province = "";
        $scope.ZipCode = "";
        $scope.FName = "";
        $scope.LName = "";
        $scope.newPatientPatientNo = "";                    
        $scope.MobileNo = "";
        $scope.EmailId = "";
        $scope.DOB = "";
        $scope.Alergies = "";
        isNewPatient = false;
    }
    
});