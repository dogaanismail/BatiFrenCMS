using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfRoleDal : EfEntityRepositoryBase<Role, BatiFrenDBEntities>, IRoleDal
    {
    }
}
