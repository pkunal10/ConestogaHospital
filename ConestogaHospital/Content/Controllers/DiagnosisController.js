app.controller("DiagnosisCntrl", function ($scope, growl, DiagnosisService, Pubnub) {

    $("#divLoading").hide();
    getTokenDetails();
    $scope.selectedRoom = null;
    $scope.finalSelectedRoom = null;
    $scope.isAdmit = false;
    $scope.Symptoms = [];
    $scope.Prescriptions = [];

    function getTokenDetails() {
        DiagnosisService.getTokenDetails().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.tokens = data.data.data.tokenList;
                $scope.totalTokens = data.data.data.tokenList.length;
                $scope.todayDate = data.data.data.today;
            }
            else {
                $scope.tokens = "";
                $scope.totalTokens = "";
                $scope.todayDate = "";
                growl.error(data.data.msg, { title: 'Error!', ttl: 3000 });
            }
        });
    }
    function getPatientAlergiesAndLastDiagnosis(patientId) {
        DiagnosisService.getPatientAlergiesAndLastDiagnosis(patientId).then(function (data) {
            if (data.data.Status == "Success") {
                $scope.allergyList = data.data.data.alergies;
                $scope.lastDiagnosis = data.data.data.lastDiagnosis;
            }
        })
    }

    $scope.addSymptom = function () {
        $scope.Symptoms.push({ 'SymptomName': '' });
    }
    $scope.removeSymptom = function (symptom) {
        var index = $scope.Symptoms.indexOf(symptom);
        $scope.Symptoms.splice(index, 1);
    };

    $scope.addPrescription = function () {
        $scope.Prescriptions.push({ 'PrescriptionName': '' });
    }
    $scope.removePrescription = function (prescription) {
        var index = $scope.Prescriptions.indexOf(prescription);
        $scope.Prescriptions.splice(index, 1);
    };

    $scope.saveDiagnosis = function () {
        if ($scope.underDiagnosisToken == null) {
            growl.error("No token is selected.", { title: 'Error!', ttl: 3000 });
            return;
        }
        if ($scope.isAdmit == true) {
            if ($scope.finalSelectedRoom == null) {
                growl.error("No room is selected.", { title: 'Error!', ttl: 3000 });
                return;
            }
            else {
                if ($scope.finalSelectedRoom.IsAvailable == false) {
                    growl.error("Selected room is not available.", { title: 'Error!', ttl: 3000 });
                    return;
                }
            }
        }
        if ($scope.Symptoms.length == 0) {
            growl.error("Enter symptoms.", { title: 'Error!', ttl: 3000 });
            return;
        }
        if ($scope.Prescriptions.length == 0) {
            growl.error("Enter prescriptions.", { title: 'Error!', ttl: 3000 });
            return;
        }
        for (var i = 0; i < $scope.Symptoms.length; i++) {
            if ($scope.Symptoms[i].SymptomName == "" || $scope.Symptoms[i].SymptomName == undefined) {
                var pos = i + 1;
                growl.error("Enter symptom at " + pos + " position.", { title: 'Error!', ttl: 3000 });
                return false;
            }
        }
        for (var i = 0; i < $scope.Prescriptions.length; i++) {
            if ($scope.Prescriptions[i].PrescriptionName == "" || $scope.Prescriptions[i].PrescriptionName == undefined) {
                var pos = i + 1;
                growl.error("Enter prescription at " + pos + " position.", { title: 'Error!', ttl: 3000 });
                return false;
            }
        }
        patAdmitDetais = null;
        if ($scope.isAdmit == true) {
            patAdmitDetais = {
                PatientId: $scope.underDiagnosisToken.PatientId,
                RoomId: $scope.finalSelectedRoom.RoomId
            }
        }
        diagnosis = {
            PatientId: $scope.underDiagnosisToken.PatientId
        }
        token = {
            TokenId: $scope.underDiagnosisToken.TokenId
        }

        model = {
            Symptoms: $scope.Symptoms,
            Prescriptions: $scope.Prescriptions,
            IsAdmit: $scope.isAdmit,
            PatientAdmitDetail: patAdmitDetais,
            Diagnosis: diagnosis,
            Token: token

        }
        DiagnosisService.saveDiagnosis(model).then(function (data) {

            if (data.data.Status = "Success") {
                $scope.underDiagnosisToken = null;
                $scope.isAdmit = false;
                $scope.finalSelectedRoom = null;
                $scope.selectedRoom = null;
                $scope.lastDiagnosis = null;
                $scope.allergyList = null;
                getTokenDetails();
                growl.success('Diagnosis saved.', { title: 'Success', ttl: 3000 });
            }

        })
    }

    $scope.saveRoom = function (room) {
        if (room.IsAvailable == false) {
            growl.error('This room is not available.', { title: 'Error!', ttl: 3000 });
        }
        else {
            for (var i = 0; i < $scope.rooms.length; i++) {
                $scope.rooms[i].color = "#333";
            }
            $scope.selectedRoom = room;
            $scope.selectedRoom.color = "#5cb85c";
        }
    }

    $scope.saveSelectedRoom = function () {
        if ($scope.selectedRoom == null) {
            growl.error('Please select room first.', { title: 'Error!', ttl: 3000 });
        }
        else {
            $scope.finalSelectedRoom = $scope.selectedRoom;
            $scope.isAdmit = true;
            $("#roomsModal").modal('hide');
        }
    }

    $scope.clearSelectedRoom = function () {
        $scope.isAdmit = false;
        $scope.selectedRoom = null;
        $scope.finalSelectedRoom = null;
    }

    $scope.fetchRoomNos = function () {
        $("#divLoading").show();
        $("#roomsModal").modal('show');
        DiagnosisService.fetchRoomNos().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.rooms = "";
                $scope.floors = "";
                $scope.rooms = data.data.data.list;
                $scope.floors = data.data.data.floors;
                $("#divLoading").hide();
            }
        })
    }

    $scope.sayIt = function () {
        window.speechSynthesis.speak(new SpeechSynthesisUtterance("Token number DM-12 in room 111 to doctor damini rajput."));
        //window.speechSynthesis.speak(new SpeechSynthesisUtterance("Damu damu damu."));
    }
    function speak(text) {
        window.speechSynthesis.speak(new SpeechSynthesisUtterance(text));
    }

    $scope.makeUnderDiagnosis = function (id) {
        DiagnosisService.makeUnderDiagnosis(id).then(function (data) {
            if (data.data.Status == "Success") {
                $scope.underDiagnosisToken = data.data.data;
                $("#tokenModal").modal('hide');
                getTokenDetails();
                getPatientAlergiesAndLastDiagnosis($scope.underDiagnosisToken.PatientId);
            }
        }), function () {
            growl.error('Something went wrong.', { title: 'Error!', ttl: 3000 });
        }
    }
    $scope.announceToken = function (id) {
        DiagnosisService.announceToken(id).then(function (data) {
        });
    }
});