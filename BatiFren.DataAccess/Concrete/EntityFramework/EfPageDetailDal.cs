using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;

namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfPageDetailDal :  EfEntityRepositoryBase<PageDetail, BatiFrenDBEntities>, IPageDetailDal
    {
    }
}
