using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfPageDal :  EfEntityRepositoryBase<Page, BatiFrenDBEntities>, IPageDal
    {
    }
}
