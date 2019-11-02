using BatiFren.Entities;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IUserPermissionService
    {
        List<UserPermission> GetList();
        UserPermission GetByPermissionId(int id);
        void Add(UserPermission permission);
        void Delete(UserPermission permission);
        void Update(UserPermission permission);
    }
}
