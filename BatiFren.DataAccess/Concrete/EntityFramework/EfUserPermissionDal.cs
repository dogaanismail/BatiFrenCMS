using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;

namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfUserPermissionDal :  EfEntityRepositoryBase<UserPermission, BatiFrenDBEntities>, IUserPermissionDal
    {
    }
}
