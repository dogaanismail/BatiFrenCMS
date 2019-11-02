using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfModuleDal : EfEntityRepositoryBase<Module, BatiFrenDBEntities>, IModuleDal
    {
    }
}
