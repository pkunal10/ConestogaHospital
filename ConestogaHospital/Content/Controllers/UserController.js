app.controller("UserCntrl", function ($scope, growl, UserService) {

    $("#divLoading").hide();
    GetRoleList();

    function GetRoleList() {
        UserService.getrolelist().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.roleList = data.data.data;
            }
        });
    }
    function GetSpecialityList() {
        UserService.getspecialtylist().then(function (data) {
            if (data.data.Status == "Success") {
                $scope.specialityList = data.data.data;
            }
        });
    }

    $scope.roleChange = function () {
        for (var i = 0; i < $scope.roleList.length; i++) {
            if ($scope.RoleId == null) {
                $scope.isDoctor = false;
                $scope.specialityList = "";
            }
            else if ($scope.roleList[i].Value == $scope.RoleId) {
                if ($scope.roleList[i].Text == "Doctor") {
                    $scope.isDoctor = true;
                    GetSpecialityList();
                }
                else {
                    $scope.isDoctor = false;
                    $scope.specialityList = "";
                }
            }
        }
    }

    //function GetAllUsers() {
    //    UserService.getAllUsers().then(function (data) {

    //        if(data.data.Status=="Success")
    //        {
    //            $scope.UserList = data.data.data;
    //        }
    //        else {
    //            $scope.NoData = true;
    //        }

    //    });
    //}

    //$scope.SetViewBy = function () {
    //    $scope.itemsPerPage = $scope.ViewBy;
    //}

    $scope.selectFileforUpload = function (files) {

        $scope.file = files[0];

    };

    $scope.MobileChange = function () {
        $scope.Password = $scope.Mobile;
    }

    $scope.SaveData = function () {

        if ($scope.Msg == "User Name NOT Available") {
            growl.error("User Name already exists", { title: 'Error!', ttl: 2000 });
            $scope.UserName = "";
            return false;
        }
        if ($scope.UserName == "" || $scope.UserName == undefined) {
            growl.error("Enter user name", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.RoleId == 0 || $scope.RoleId == undefined) {
            growl.error("Select Role", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.FName == "" || $scope.FName == undefined) {
            growl.error("Enter first name", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.LName == "" || $scope.LName == undefined) {
            growl.error("Enter last name", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.isDoctor == true) {
            if ($scope.SpecialityId == 0 || $scope.SpecialityId == undefined) {
                growl.error("Select Speciality", { title: 'Error!', ttl: 2000 });
                return false;
            }
            if ($scope.RoomNo == 0 || $scope.RoomNo == undefined) {
                growl.error("Enter room no", { title: 'Error!', ttl: 2000 });
                return false;
            }
        }
        if ($scope.Line1 == "" || $scope.Line1 == undefined) {
            growl.error("Enter Line 1 ", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.City == "" || $scope.City == undefined) {
            growl.error("Enter city ", { title: 'Error!', ttl: 2000 });
            return false;
        }

        if ($scope.Province == "" || $scope.Province == undefined) {
            growl.error("Enter Province", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.ZipCode == "" || $scope.ZipCode == undefined) {
            growl.error("Enter zip code", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.EmailId == "" || $scope.EmailId == undefined) {
            growl.error("Enter email id", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.Mobile == "" || $scope.Mobile == undefined) {
            growl.error("Enter mobile", { title: 'Error!', ttl: 2000 });
            return false;
        }
        if ($scope.DOB == "" || $scope.DOB == undefined) {
            growl.error("Select DOB", { title: 'Error!', ttl: 2000 });
            return false;
        }

        var month = $scope.DOB.getMonth();
        month++;
        var dob = $scope.DOB.getDate() + "/" + month + "/" + $scope.DOB.getFullYear();
        $("#divLoading").show();
        UserService.addUser($scope.UserName, $scope.RoleId, $scope.Password, $scope.file, $scope.FName, $scope.LName, dob, $scope.SpecialityId | 0, $scope.RoomNo, $scope.Mobile, $scope.EmailId, $scope.isDoctor, $scope.Line1, $scope.City, $scope.Province, $scope.ZipCode).then(function (data) {
            if (data.data.Status == "Error") {
                $("#divLoading").hide();
                //alert(data.data.msg);
                growl.error(data.data.msg, { title: 'Error!', ttl: 2000 });
            }
            else if (data.data.Status == "Success") {
                $("#divLoading").hide();
                growl.success(data.data.msg, { title: 'Success', ttl: 2000 });                
                $scope.Clear();
            }


        }, function () {
            $("#divLoading").hide();
            growl.error("Data is not saved", { title: 'Error!', ttl: 2000 });
        });
    }

    $scope.Clear = function () {

        $scope.UserId = 0;
        $scope.RoleId = 0;
        $scope.UserName = "";
        $scope.FName = "";
        $scope.LName = "";
        $scope.DOB = "";
        $scope.isDoctor = false;
        $scope.SpecialityId = 0;
        $scope.RoomNo = "";
        $scope.Line1 = "";
        $scope.City = "";
        $scope.Province = "";
        $scope.ZipCode = "";
        $scope.EmailId = "";
        $scope.Mobile = "";
        $scope.Password = "";
        $scope.ClassOfUNameTB = "";
        $scope.ClassOfUNameMsg = "";
        angular.element("input[type='file']").val(null);
        $scope.Msg = "";
        GetRoleList();
    }


    $scope.CheckAvailability = function () {
        if ($scope.UserName == "" || $scope.UserName == null) {
            $scope.Msg = "";
            $scope.ClassOfUNameTB = "";
            $scope.ClassOfUNameMsg = "";
            return false;
        }
        UserService.checkAvailability($scope.UserName).then(function (data) {
            if (data.data.Status == "Success") {
                $scope.Msg = "User Name Available";
                $scope.ClassOfUNameTB = "has-success";
                $scope.ClassOfUNameMsg = "fa fa-check";
                $scope.Color = "green";
            }
            else {
                $scope.Msg = data.data.msg;
                $scope.ClassOfUNameTB = "has-error";
                $scope.ClassOfUNameMsg = "fa fa-times-circle-o";
                $scope.Color = "red";
            }
        });
    }

});