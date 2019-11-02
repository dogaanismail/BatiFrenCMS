using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatiFren.Business.Concrete
{
    public class UserPermissionManager : IUserPermissionService
    {
        private IUserPermissionDal _permissionDal;
        public UserPermissionManager(IUserPermissionDal permissionDal)
        {
            _permissionDal = permissionDal;
        }
        public void Add(UserPermission permission)
        {
            _permissionDal.Add(permission);
        }

        public void Delete(UserPermission permission)
        {
            _permissionDal.Delete(permission);
        }

        public UserPermission GetByPermissionId(int id)
        {
            return _permissionDal.Find(x => x.UserPermissionID == id);
        }

        public List<UserPermission> GetList()
        {
            return _permissionDal.GetList();
        }

        public void Update(UserPermission permission)
        {
             _permissionDal.Update(permission);
        }
    }
}
