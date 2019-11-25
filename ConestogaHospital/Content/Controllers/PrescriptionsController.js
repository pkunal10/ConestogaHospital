app.controller("PrescriptionCntrl", function ($scope, growl, PrescriptionsService) {

    $("#divLoading").hide();
    getPatientNoForAutoComplete();

    function getPatientNoForAutoComplete() {

        PrescriptionsService.getPatientNoForAutoComplete().then(function (data) {

            if (data.data.Status == "Success") {
                $scope.patientNoList = data.data.data;
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

    $scope.viewPreSym = function (id) {
        $("#divLoading").show();
        $("#presModal").modal('show');
        PrescriptionsService.viewPres(id).then(function (data) {
            if (data.data.Status == "Success") {                
                $scope.Symptoms = data.data.data.Symptoms;
                $scope.Prescriptions = data.data.data.Prescriptions;
                $("#divLoading").hide();
            }
        })
    }

    $scope.Clear = function () {
        $scope.PatientNo = "";
        $scope.diagnosisList = "";
    }

    $scope.getDiagnosisList = function () {
        if ($scope.PatientNo == '' || $scope.PatientNo == undefined) {
            growl.error("Enter patient no.", { title: 'Error!', ttl: 3000 });
            $scope.diagnosisList == "";
            return;
        }
        $("#divLoading").show();
        PrescriptionsService.getDiagnosisList($scope.PatientNo).then(function (data) {
            if (data.data.Status == "Success") {
                $("#divLoading").hide();
                $scope.diagnosisList = data.data.data;

            }
            else {
                $("#divLoading").hide();
                growl.error(data.data.msg, { title: 'Error!', ttl: 3000 });
                $scope.PatientNo == "";
                $scope.diagnosisList == "";
            }
        })
    }

});