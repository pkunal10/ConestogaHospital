app.controller("AdmitPatientCntrl", function ($scope, growl, AdmitPatientListService) {

    $("#divLoading").hide();
    getAdmitPatientList();
    

    function getAdmitPatientList() {
        $("#divLoading").show();
        AdmitPatientListService.getAdmitPatientList().then(function (data) {
            $("#divLoading").hide();
            if (data.data.Status == "Success") {
                $scope.patientList = data.data.data;
            }
            else {
                growl.error(data.data.msg, { title: 'Error!', ttl: 3000 });
            }

        })

    }
    
    $scope.dischargePatient = function (detailsId) {
        $("#divLoading").show();
        AdmitPatientListService.dischargePatient(detailsId).then(function (data) {
            if (data.data.Status == "Success") {
                $("#divLoading").hide();
                growl.success("Patient is discharged.", { title: 'Success', ttl: 3000 });
                getAdmitPatientList();
            }            
        })
    }
    
});