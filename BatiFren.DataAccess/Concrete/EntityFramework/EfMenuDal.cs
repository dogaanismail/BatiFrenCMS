using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfMenuDal :  EfEntityRepositoryBase<Menu, BatiFrenDBEntities>, IMenuDal 
    {
    }
}
