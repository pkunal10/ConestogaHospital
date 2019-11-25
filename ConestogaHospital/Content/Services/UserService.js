app.service("UserService", function ($http) {


    this.getrolelist = function () {
        var response = $http({
            method: "post",
            url: "GetRoleList"
        });
        return response;
    }

    this.getspecialtylist = function () {
        var response = $http({
            method: "post",
            url: "GetSpecialityList"
        });
        return response;
    }


    this.UpdateData = function (model) {
        var response = $http({
            method: "post",
            url: "UpdateRoleName",
            data: model,
            dataType: JSON.stringify
        });
        return response;
    };

    this.addUser = function (userName, roleId, password, file, fname, lname, dob, specialityId, roomNo, mobile, email, isDoctor,line1,city,province,zipCode) {
        var formData = new FormData();
        formData.append("UserName", userName);
        formData.append("RoleId", roleId);
        formData.append("Password", password);
        formData.append("files", file);
        formData.append("FName", fname);
        formData.append("LName", lname);
        formData.append("DOB", dob);
        formData.append("SpecialityId", specialityId);
        formData.append("RoomNo", roomNo);
        formData.append("Mobile", mobile);
        formData.append("EmailId", email);
        formData.append("ISDoctor", isDoctor);
        formData.append("Line1", line1);
        formData.append("City", city);
        formData.append("Province", province);
        formData.append("ZipCode", zipCode);
        var response = $http.post("/Admin/AddUser", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            });
        return response;
    }

    this.leftJobConfirYesBtn = function (id) {
        var response = $http({
            method: "post",
            url: "LeftJobConfirYesBtn",
            params: {
                Id: id
            }
        });
        return response;
    }

    this.checkAvailability = function (userName) {
        var response = $http({
            method: "post",
            url: "CheckAvailability",
            params: {
                UserName: userName,
            }
        })
        return response;
    }

    this.getAllUsers = function () {
        var response = $http({
            method: "post",
            url: "GetAllUsers",
        });

        return response;
    }

    this.getUserData = function (id) {
        var response = $http({
            method: "post",
            url: "GetUserData",
            params: {
                Id: id
            }
        });
        return response;
    }

    this.editData = function (id) {

        var response = $http({
            method: "post",
            url: "EditData",
            params: {
                Id: id,
            }
        });
        return response;

    }

    this.deleteData = function (id) {

        var response = $http({
            method: "post",
            url: "DeleteData",
            params: {
                Id: id,
            }
        });
        return response;

    }

    this.refreshRights = function (model) {
        var response = $http({
            method: "post",
            url: "RefreshRights",
            data: model,
            dataType: JSON.stringify
        });
        return response;
    }

});