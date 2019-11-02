//Menu Controller
app.controller('MenuCtrl', function ($scope, $http) {
    //Get login user role
    $scope.init = function () {
        var RoleID = parseInt(document.getElementById('RoleId').value);

        params = {
            RoleID: RoleID
        };
        //Get role vise menu for display
        $http.get("/api/Menu", { params: params }).then(function (result) {
            $scope.lstMenu = result.data;
            $scope.lstParentMenu = [];
            $scope.lstSubMenu = [];
            console.log(result);

            //make a main menu and a sub menu different list
            for (var i = 0; i < result.data.length; i++) {
                if (result.data[i].ParentModuleID === 0) {
                    $scope.lstParentMenu.push(result.data[i]);
                }
                else {
                    $scope.lstSubMenu.push(result.data[i]);
                }
            }

            setTimeout(function () {
                $scope.$apply(function () {
                    //Set submenu list in main menu 
                    for (var j = 0; j < $scope.lstParentMenu.length; j++) {
                        $scope.lstParentMenu[j].Submenu = [];

                        for (var k = 0; k < $scope.lstSubMenu.length; k++) {
                            if ($scope.lstParentMenu[j].ModuleID === $scope.lstSubMenu[k].ParentModuleID) {
                                $scope.lstParentMenu[j].Submenu.push($scope.lstSubMenu[k]);
                                console.log($scope.lstSubMenu[k]);
                            }
                        }
                    }
                });
            }, 500);

        });
    };
    $scope.init();
});

//User Controller
app.controller('UserCtrl', function ($scope, $http) {
    //Initialize data
    $scope.init = function () {
        $scope.model = {
            UserID: 0,
            RoleID: '',
            FirstName: '',
            LastName: '',
            PhotoPath: '',
            Email: '',
            UserName: '',
            Password: '',
            LastLoginDate: '',
            CreatedDate: '',
            IsActive: false
        };
     
        //for User Detail
        $scope.flgflgUserDetail = false;

        //for User list
        $scope.flgTable = true;

        //for display message of user
        $scope.flgMessage = false;
      
        $scope.flgUser = true;

        $scope.showCreate = false;

        $scope.showEdit = false;

        //for showing message
        $scope.flgMessage1 = false;
        $scope.message = "";
        $scope.message1 = "";

        //for User link
        $scope.UserState = "";

        $scope.getAllUser();
        $scope.getAllRoles();
      
    };

    //hide message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");

        $scope.flgMessage1 = false;
        $("#message1").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon1").removeClass("fa-check").removeClass("fa-ban");
    };

    //GetAllUser
    $scope.getAllUser = function () {

        var table = $("#userTable").DataTable();
        table.clear();
        table.destroy();
        $http({
            method: 'GET',
            url: '/api/Users'
        }).then(function (result) {
            $scope.lstUsers = result.data;

            setTimeout(function () {
                $('#userTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);

        });
       
    };

    //Open user form
    $scope.addUser = function () {
        //make table flg false for dispaly message
        $scope.flgTable = false;
        $scope.showCreate = true;
        $scope.showEdit = false;
        
        $scope.UserState = "> Add User";
    };

    //Edit User
    $scope.editUser = function (obj) {
        $scope.model.UserID = obj.UserID;
        $scope.model.RoleID = obj.RoleID;
        $scope.model.FirstName = obj.FirstName;
        $scope.model.LastName = obj.LastName;
        $scope.model.PhotoPath = obj.PhotoPath;
        $scope.model.Email = obj.Email;
        $scope.model.UserName = obj.UserName;
        $scope.model.Password = obj.Password;
        $scope.model.LastLoginDate = obj.LastLoginDate;
        $scope.model.CreatedDate = obj.CreatedDate;
        $scope.model.IsActive = obj.IsActive;
        $scope.flgTable = false;
        $scope.showCreate = false;
        $scope.showEdit = true;

        //For Uploading User profile picture
        $scope.Url = "/api/UploadFile?UserId=" + obj.ID;
        $scope.UserState = " > Edit";
    };

    //Create/update User
    $scope.createUser = function (obj) {
        $scope.hideMessage();

        if ($scope.showCreate === true)
        {
            //Check password and confirm password is same
            if (obj.Password !== obj.CPassword) {
                $scope.flgMessage = true;
                $scope.message = "Password and Confirm Password must be same.";
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
            else {
                $http.post("/api/Users", obj).then(function (result) {
                    if (result.data.success === 1) {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("glyphicon glyphicon-ok");
                    }
                    else {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                    }
                });
            }
        }
        else {
            $http.post("/api/Users", obj).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("glyphicon glyphicon-ok");
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                }
            });
        }
  
};

    //Delete User
    $scope.deleteUser = function (obj) {
        $scope.hideMessage();
        params = {
            ID: obj.UserID
        }
        $http.delete("/api/Users", { params: params }).then(function (result) {
            if (result.data.success===1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllUser();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Get all roles
    $scope.getAllRoles = function () {
        $http.get("/api/Roles").then(function (result) {
            $scope.lstRoles = result.data;
        });
    };

    function formatDate(d) {
        date = new Date(d);
        var dd = date.getDate();
        var mm = date.getMonth() + 1;
        var yyyy = date.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm };
        return d = yyyy + '-' + mm + '-' + dd;
    }

    //Reset data
    $scope.reset = function () {
        $scope.UserState = "";
        $scope.flgTable = true;
        $scope.flgflgUserDetail = false;
        $scope.flgUserDetail = false;
        $scope.flgUser = true;
        $scope.model = {
            UserID: 0,
            RoleID: '',
            FirstName: '',
            LastName: '',
            PhotoPath: '',
            Email: '',
            UserName: '',
            Password: '',
            LastLoginDate: '',
            CreatedDate: '',
            IsActive: false
        },
       $("#liTab_2").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#liTab_1").addClass("active");
        $("#tab_1").addClass("active");
    };

    //Set Image
    $scope.Image = function (e) {
        $("#imgs").attr('src', URL.createObjectURL(e.target.files[0]));
    };


    $scope.init();
});

//Role Controller
app.controller('RoleCtrl', function ($scope, $http) {
    //Initialize data
    $scope.init = function () {

        $scope.model = {
            RoleID: '',
            RoleName: '',
            DisplayName: '',
            Description: '',
            InsertDate: '',
            LastModifiedDate:'',
            UserPermission: [],
        };
     
        //for Display Role list
        $scope.flgTable = true;

        //for Display Message
        $scope.flgMessage = false;

        //for message
        $scope.message = "";

        //for Role Link
        $scope.UserState = "";
   
        $scope.getAllRoles();
        $scope.getAllModule();

        //Defined checked Module
        $scope.checkedModule = {
            module: []
        };
    };

    //Hide alert message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");
    };

    //Get all module
    $scope.getAllModule = function () {
        $http.get("/api/Roles?ID=null").then(function (result) {
            $scope.lstModule = _.sortBy(result.data, 'id');
        });
    };

    //Get all roles
    $scope.getAllRoles = function () {
        //Define table as Datatable
        var table = $("#roleTable").DataTable();
        //Clear table
        table.clear();
        //Destroy table
        table.destroy();

        $http.get("/api/Roles").then(function (result) {
            $scope.lstRoles = result.data;

            //Set table Configuration
            setTimeout(function () {
                $("#roleTable").DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);
        });
    };
  

    //Change flag for dispaly form
    $scope.addRole = function () {
        //Make Table flg false for display Add role form
        $scope.flgTable = false;
        //for Display Link
        $scope.UserState = "> Add Role";
    };

    //Edit role
    $scope.editRole = function (obj) {
        $scope.model.RoleID = obj.RoleID;
        $scope.model.RoleName = obj.RoleName;
        $scope.model.DisplayName = obj.DisplayName;
        $scope.model.Description = obj.Description;
        //For display role form
        $scope.flgTable = false;
        $scope.UserState = "> Edit Role";
        //Get permission and showing in checkbox
        params = {
            id: obj.RoleID
        };
        $http.get("/api/Permissions", { params: params }).then(function (result) {
            result.data = _.sortBy(result.data, 'ModuleID');
            for (var i = 0; i < result.data.length; i++) {
                for (var j = 0; j < $scope.lstModule.length; j++) {
                    if ($scope.lstModule[j].ModuleID === result.data[i].ModuleID) {
                        if (!_.contains($scope.checkedModule.module, $scope.lstModule[j])) {
                            $scope.checkedModule.module.push($scope.lstModule[j]);
                        }
                    }
                }
            }
           
        });
    };

    //Create role
    $scope.createRole = function (obj) {
        $scope.hideMessage();
        $http.post("/api/Roles", obj).then(function (result) {
            if (result.data.success===1) {
                //made message flag true for display message and add classes
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllRoles();
                $scope.reset();
            }
            else {
                //made message flag true for display message and add classes
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Delete role
    $scope.deleteRole = function (obj) {
        $scope.hideMessage();
        params = {
            ID: obj.RoleID
        };
        $http.delete("/api/Roles", { params: params }).then(function (result) {
            if (result.data.success===1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllRoles();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Create Role Permission
    $scope.createRolePermission = function (obj) {
        //Make checked module list
        var list = [];

        for (var i = 0; i <= obj.length; i++) {
            if (i === obj.length) {
                $scope.model.UserPermission = list;
                $http.post("/api/Roles", $scope.model).then(function (result) {
                    if (result.data.success===1) {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("glyphicon glyphicon-ok");
                        $scope.getAllRoles();
                        $scope.reset();
                    }
                    else {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("glyphicon glyphicon-warning-sign");

                    }
                });
            } else {
                var obj1 = new Object();
                obj1.ModuleID = 0;
                obj1.ModuleID = obj[i].ModuleID;
                obj1.RoleID = 0;
                list.push(obj1);
            }
        }
    };

    //Open permission tab
    $scope.setPermission = function (obj) {
        $("#liTab_1").removeClass("active");
        $("#tab_1").removeClass("active");
        $("#liTab_2").addClass("active");
        $("#tab_2").addClass("active");
    };

    //Reset model
    $scope.reset = function () {
        $scope.checkedModule.module = [];
        $scope.flgTable = true;
        $scope.UserState = "";
        //$scope.flgMessage = false;
        $scope.model = {
            RoleID: '',
            RoleName: '',
            DisplayName: '',
            Description: '',
            InsertDate: '',
            LastModifiedDate: '',
            UserPermission: [],
        };
        $("#liTab_2").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#liTab_1").addClass("active");
        $("#tab_1").addClass("active");
    };

    $scope.init(); 

});

//Module Controller
app.controller('ModuleCtrl', function ($scope, $http) {

    //Initialize data
    $scope.init = function () {
        $scope.model = {
            ModuleID: '',
            DisplayOrder: '',
            ModuleName: '',
            PageIcon: '',
            PageUrl: '',
            PageSlug: '',
            IsDeleted: false,
            IsActive: true,
            IsDefault: false,
            ParentModuleID: 0
        };
      
        //for module link
        $scope.flgTable = true;
        //for display message
        $scope.flgMessage = false;
        //for message
        $scope.message = "";
        //for User link
        $scope.UserState = "";

        $scope.getAllModule();

        //Get all module
        $http.get("/api/Roles?ID=null").then(function (result) {
            $scope.lstModuleDropdown = _.sortBy(result.data, 'id');
        });
    };

    //Hide alert message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");
    };

    //Get all module
    $scope.getAllModule = function () {
        //Define DataTable
        var table = $("#moduleTable").DataTable();
        table.clear();
        table.destroy();

        $http.get("/api/Modules").then(function (result) {
            $scope.lstModule = result.data;

            //Set Table Configuration
            setTimeout(function () {
                $('#moduleTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);
        });
    };

    //Open module form
    $scope.addModule = function () {
        //make table flg false for Display Add Module form
        $scope.flgTable = false;
        $scope.UserState = "> Add Module";
    };

    //Edit module data
    $scope.editModule = function (obj) {
        //Edit Module
        $scope.UserState = "> Edit Module";
        $scope.model.ModuleID = obj.ModuleID;
        $scope.model.DisplayOrder = obj.DisplayOrder;
        $scope.model.ModuleName = obj.ModuleName;
        $scope.model.PageIcon = obj.PageIcon;
        $scope.model.PageUrl = obj.PageUrl;
        $scope.model.PageSlug = obj.PageSlug;
        $scope.model.IsDeleted = obj.IsDeleted;
        $scope.model.IsActive = obj.IsActive;
        $scope.model.IsDefault = obj.IsDefault;
        $scope.model.ParentModuleID = obj.ParentModuleID;
        $scope.flgTable = false;
    };

    //Create/update module
    $scope.createModule = function (obj) {
        $scope.hideMessage();
        $http.post("/api/Modules", obj).then(function (result) {
            if (result.data.success===1) {
                //Make message flg true for display message
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllModule();
                $scope.reset();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Delete Module
    $scope.deleteModule = function (obj) {
        $scope.hideMessage();
        params = {
            ID: obj.ModuleID
        };
        $http.delete("/api/Modules", { params: params }).then(function (result) {
            if (result.data.success===1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllModule();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Reset data
    $scope.reset = function () {
        $scope.flgTable = true;
        $scope.UserState = "";
        $scope.model = {
            ModuleID: '',
            DisplayOrder: '',
            ModuleName: '',
            PageIcon: '',
            PageUrl: '',
            PageSlug: '',
            IsDeleted: false,
            IsActive: true,
            IsDefault: false,
            ParentModuleID: 0
        };
    };

    $scope.init(); 
});

