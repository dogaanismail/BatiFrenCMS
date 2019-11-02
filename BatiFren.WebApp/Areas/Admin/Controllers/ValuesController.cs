using BatiFren.Business.Abstract;
using System.Collections.Generic;
using System.Web.Http;
using BatiFren.Business.DependencyResolvers.Ninject;
using BatiFren.Entities;
using BatiFren.Entities.EntityClasses;
using System.Data.Entity;
using System.Linq;
using System;

namespace BatiFren.WebApp.Areas.Admin.Controllers
{

    #region ModulesController
    public class ModulesController : ApiController
    {
        BatiFrenDBEntities db = new BatiFrenDBEntities();
        Dictionary<string, object> res = new Dictionary<string, object>();
        GeneralHelper generalHelper = new GeneralHelper();

        //Get module api
        public IEnumerable<Modules> Get()
        {
            List<Modules> module = new List<Modules>();
            module = db.Modules.Where(x => x.IsDeleted == false).Select(x => new Modules()
            {
                ModuleID = x.ModuleID,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault,
                IsDeleted = x.IsDeleted,
                ModuleName = x.ModuleName,
                PageIcon = x.PageIcon,
                PageSlug = x.PageSlug,
                PageUrl = x.PageUrl,
                ParentModuleID = x.ParentModuleID
            }).ToList();
            return module;
        }

        //insert/update module api
        public IHttpActionResult Post(Module module)
        {
            try
            {
                if (module.ModuleID == 0)
                {
                    if (db.Modules.Any((x => x.ModuleName == module.ModuleName && x.IsDeleted == false)))
                    {
                        res["success"] = 0;
                        res["message"] = "Module already exist.";
                    }
                    else
                    {
                        //get Module Slug
                        module.PageSlug = generalHelper.GetModuleSlug(module.ModuleName, Convert.ToInt32(module.ParentModuleID));
                        module.IsActive = true;
                        module.IsDefault = false;
                        module.IsDeleted = false;
                        db.Modules.Add(module);
                        db.SaveChanges();
                        res["success"] = 1;
                        res["message"] = "Module created successfully.";
                    }
                }
                else
                {
                    if (db.Modules.Any((x => x.ModuleName == module.ModuleName && x.ModuleID != module.ModuleID && x.IsDeleted == false)))
                    {
                        res["success"] = 0;
                        res["message"] = "Module already exist.";
                    }
                    else
                    {
                        if (module.IsDefault == true)
                        {
                            res["success"] = 0;
                            res["message"] = "Module can not update.";
                        }
                        else
                        {
                            db.Entry(module).State = EntityState.Modified;
                            db.SaveChanges();
                            res["success"] = 1;
                            res["message"] = "Module updated Successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //delete module by id api
        public IHttpActionResult Delete(int ID)
        {
            try
            {
                //Check Module references
                if (db.UserPermissions.Any(x => x.ModuleID == ID))
                {
                    res["success"] = 0;
                    res["message"] = "Module can not delete, It has reference to another data.";
                }
                else
                {
                    Module module = db.Modules.FirstOrDefault(x => x.ModuleID == ID);
                    if (module != null)
                    {
                        if (module.IsDefault == true)
                        {
                            res["success"] = 0;
                            res["message"] = "Module can not delete.";
                        }
                        else
                        {
                            db.Entry(module).State = EntityState.Deleted;
                            db.SaveChanges();
                            res["success"] = 1;
                            res["message"] = "Module deleted successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }


    }
    #endregion

    #region UsersController
    public class UsersController : ApiController
    {
        private IUserService _userService = InstanceFactory.GetInstance<IUserService>();
        BatiFrenDBEntities dB = new BatiFrenDBEntities();
        GeneralHelper generalhelper = new GeneralHelper();
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Get user api
        public IEnumerable<Users> Get()
        {
            List<Users> userList = _userService.GetList().Where(x => x.IsActive == true).Select(x => new Users()
            {
                UserID = x.UserID,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Password = x.Password,
                PhotoPath = x.PhotoPath,
                RoleID = x.RoleID,
                LastLoginDate = x.LastLoginDate,
                UserName = x.UserName,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            }).ToList();

            return userList;
        }

        //insert/update user api
        public IHttpActionResult Post(User user)
        {
            //Finding the user
            User getUser = _userService.GetByUserId(user.UserID);

            try
            {
                if (getUser == null)
                {
                    if (dB.Users.Any(x => x.UserName == user.UserName))
                    {
                        res["success"] = 0;
                        res["message"] = "Username is already exist.";
                    }
                    else if (dB.Users.Any(x => x.Email == user.Email))
                    {
                        res["success"] = 0;
                        res["message"] = "Email address is already exist.";
                    }
                    else
                    {
                        User newUser = new User
                        {
                            RoleID = user.RoleID,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhotoPath = user.PhotoPath,
                            UserName = user.UserName,
                            Email = user.Email,
                            Password = generalhelper.Encrypt(user.Password),
                            LastLoginDate = null,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };
                        _userService.Add(newUser);
                        res["success"] = 1;
                        res["message"] = "User created successfully.";
                    }
                }
                else
                {
                    _userService.Update(user);
                    res["success"] = 1;
                    res["message"] = "User updated Successfully.";
                }

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //Delete user by user id
        public IHttpActionResult Delete(int ID)
        {
            try
            {
                User deleteUser = _userService.GetByUserId(ID);
                if (deleteUser != null)
                {
                    _userService.Delete(deleteUser);
                    res["success"] = 1;
                    res["message"] = "User deleted successfully.";
                }
                else
                {
                    res["success"] = 1;
                    res["message"] = "The user could not be found!";
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }
    #endregion

    #region RolesController
    public class RolesController : ApiController
    {
        BatiFrenDBEntities db = new BatiFrenDBEntities();
        GeneralHelper generalHelper = new GeneralHelper();
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Roles get api
        public IEnumerable<Roles> Get()
        {
            List<Roles> role = new List<Roles>();
            role = db.Roles.Where(x => x.IsDeleted == false).Select(x => new Roles()
            {
                RoleID = x.RoleID,
                Description = x.Description,
                DisplayName = x.DisplayName,
                InsertBy = x.InsertBy,
                InsertDate = x.InsertDate,
                IsDefault = x.IsDefault,
                IsDeleted = x.IsDeleted,
                LastModifiedDate = x.LastModifiedDate,
                LastModifiedBy = x.LastModifiedBy,
                RoleName = x.RoleName
            }).ToList();
            return role;
        }

        //Get module api
        public IEnumerable<ModuleList_Result> Get(int? ID)
        {
            List<ModuleList_Result> module = db.ModuleList(ID).ToList();
            return module;
        }

        //Roles insert/update api
        public IHttpActionResult Post(Roles role)
        {
            try
            {               
                if (role.RoleID == 0)
                {
                    if (db.Roles.Any((x => x.RoleName == role.RoleName && x.IsDeleted == false)))
                    {
                        res["success"] = 0;
                        res["message"] = "Role already exist.";
                    }
                    else
                    {
                        Role objrole = new Role();
                        objrole.RoleID = role.RoleID;
                        objrole.Description = role.Description;
                        objrole.DisplayName = role.DisplayName;
                        objrole.RoleName = role.RoleName;
                        objrole.IsDeleted = false;
                        objrole.InsertDate = DateTime.Now;
                        objrole.IsDeleted = false;

                        db.Roles.Add(objrole);
                        db.SaveChanges();

                        for (var i = 0; i < role.UserPermission.Count(); i++)
                        {
                            role.UserPermission[i].RoleID = objrole.RoleID;
                            db.UserPermissions.Add(role.UserPermission[i]);
                            db.SaveChanges();
                        }

                        res["success"] = 1;
                        res["message"] = "Role created successfully.";
                    }
                }
                else
                {
                    if (db.Roles.Any((x => x.RoleName == role.RoleName && x.RoleID != role.RoleID && x.IsDeleted == false)))
                    {
                        res["success"] = 0;
                        res["message"] = "Role already exist.";
                    }
                    else
                    {
                        Role objrole = db.Roles.FirstOrDefault(x => x.RoleID == role.RoleID);
                        if (objrole.IsDefault == false)
                        {
                            res["success"] = 0;
                            res["message"] = "Module can not update.";
                        }
                        else
                        {
                            objrole.RoleID = role.RoleID;
                            objrole.Description = role.Description;
                            objrole.DisplayName = role.DisplayName;
                            objrole.RoleName = role.RoleName;
                            objrole.IsDeleted = false;
                            objrole.LastModifiedDate = DateTime.Now;
                            objrole.IsDeleted = false;

                            db.Entry(objrole).State = EntityState.Modified;
                            db.SaveChanges();

                            //Delete all User Permission
                            List<Entities.UserPermission> listPermission = db.UserPermissions.Where(x => x.RoleID == role.RoleID).ToList();
                            foreach (var objpermission in listPermission)
                                db.UserPermissions.Remove(objpermission);
                            db.SaveChanges();

                            //Create New Permission of users
                            for (var i = 0; i < role.UserPermission.Count(); i++)
                            {
                                role.UserPermission[i].RoleID = objrole.RoleID;
                                db.UserPermissions.Add(role.UserPermission[i]);
                                db.SaveChanges();
                            }

                            res["success"] = 1;
                            res["message"] = "Role updated Successfully.";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //Role delete by id api
        public IHttpActionResult Delete(int ID)
        {
            try
            {
                if (db.UserPermissions.Any(x => x.RoleID == ID))
                {
                    res["success"] = 0;
                    res["message"] = "Role can not delete, It has reference to another data !";
                }
                else
                {
                    Role role = db.Roles.Where(x => x.RoleID == ID).FirstOrDefault();
                    if (role != null)
                    {
                        if (role.IsDefault == true)
                        {
                            res["success"] = 0;
                            res["message"] = "Role can not delete because it has been created as default !";
                        }
                        else
                        {
                            db.Entry(role).State = EntityState.Deleted;
                            db.SaveChanges();
                            res["success"] = 1;
                            res["message"] = "Role deleted successfully !";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

    }
}
#endregion

    #region PermissionsController
    public class PermissionsController : ApiController
    {
        BatiFrenDBEntities db = new BatiFrenDBEntities();
        GeneralHelper generalHelper = new GeneralHelper();
        Dictionary<string, object> res = new Dictionary<string, object>();

        //Get user permission by  role id
        public IEnumerable<getUserPermission> Get(int ID)
        {
        List<getUserPermission> module = new List<getUserPermission>();
        module = db.UserPermissions.Select(x => new getUserPermission()
        {
            ID = x.UserPermissionID,
            ModuleID = x.ModuleID,
            RoleID = x.RoleID
        }).Where(x => x.RoleID == ID).ToList();
        return module;
    }
}
#endregion

    #region MenuController
    public class MenuController : ApiController
    {
        private IModuleService moduleService = InstanceFactory.GetInstance<IModuleService>();
        BatiFrenDBEntities dB = new BatiFrenDBEntities();

         public IEnumerable<getUserPermission> Get(int RoleID)
        {
            List<getUserPermission> module = new List<getUserPermission>();
            module = dB.UserPermissions.Select(x => new getUserPermission()
        {
            ID = x.UserPermissionID,
            ModuleID = x.ModuleID,
            RoleID = x.RoleID,
            ModuleName = x.Module.ModuleName,
            DisplayOrder = x.Module.DisplayOrder,
            IsActive = x.Module.IsActive,
            IsDefault = x.Module.IsDefault,
            IsDeleted = x.Module.IsDeleted,
            PageIcon = x.Module.PageIcon,
            PageSlug = x.Module.PageSlug,
            PageUrl = x.Module.PageUrl,
            ParentModuleID = x.Module.ParentModuleID
        }).Where(x => x.RoleID == RoleID &&  x.IsDeleted == false).OrderBy(x => x.ModuleID).ToList();
        return module.ToList();
    }

}

    #endregion