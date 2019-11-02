using BatiFren.Business.BusinessResult;
using BatiFren.Entities;
using BatiFren.Entities.EntityClasses;
using System.Collections.Generic;

namespace BatiFren.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetList();
        User GetByUserId(int id);
        void Add(User user);
        void Delete(User user);
        void Update(User user);
        BusinessResults<User> Login(LoginViewModel data);
    }
}
