using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IRoleService
    {
        List<Role> GetList();
        Role GetByRoleId(int id);
        void Add(Role role);
        void Delete(Role role);
        void Update(Role role);
    }
}
