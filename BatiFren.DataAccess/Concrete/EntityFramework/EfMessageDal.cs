using BatiFren.DataAccess.Abstract;
using BatiFren.Entities;
namespace BatiFren.DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal :  EfEntityRepositoryBase<Message, BatiFrenDBEntities>, IMessageDal
    {
    }
}
