using BatiFren.Business.Abstract;
using BatiFren.Business.BusinessResult;
using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
using BatiFren.Entities.EntityClasses;
using BatiFren.Entities.Messages;
using System;
using System.Collections.Generic;

namespace BatiFren.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
            public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public User GetByUserId(int id)
        {
            return _userDal.Find(x => x.UserID == id);
        }

        public List<User> GetList()
        {
            return _userDal.GetList();
        }

        public BusinessResults<User> Login(LoginViewModel data)
        {
            string depassword = "";
            GeneralHelper generalHelper = new GeneralHelper();
            depassword = generalHelper.Encrypt(data.Password);
            BusinessResults<User> IsUser = new BusinessResults<User>()
            {
                result = _userDal.GetLazyFirstOrDefault(x => x.UserName == data.UserName && x.Password == depassword, x=>x.Role, x=>x.Pages, x=>x.Messages,x=>x.PageDetails )
            };

            if (IsUser.result != null)
            {
                if (IsUser.result.IsActive == false)
                {
                    IsUser.AddError(ErrorMessageCode.UserIsNotActive, "User is not activated");
                    IsUser.AddError(ErrorMessageCode.CheckYourEmail, "Check your Email");
                }
            }
            else
            {
                IsUser.AddError(ErrorMessageCode.UsernameOrPassWrong, "Password or Username is wrong");
            }
            return IsUser;
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
