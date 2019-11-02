using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;

namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal :  EfEntityRepositoryBase<User, BatiFrenDBEntities>, IUserDal
    {
    }
}
