using BatiFren.Business.Abstract;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public void Add(Role role)
        {
            _roleDal.Add(role);
        }

        public void Delete(Role role)
        {
            _roleDal.Delete(role);
        }

        public Role GetByRoleId(int id)
        {
            return _roleDal.Find(x => x.RoleID == id);
        }

        public List<Role> GetList()
        {
            return _roleDal.GetList();
        }

        public void Update(Role role)
        {
             _roleDal.Update(role);
        }
    }
}
