using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BatiFren.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T:class, new()
    {
        List<T> GetList();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        int Add2(T entity);
        List<T> List<F>(Expression<Func<T, F>> where);   // According to order by descending query
        List<T> Query(Expression<Func<T, bool>> where);  //According to query
        List<T> GetLazyTolist(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        T GetLazyFirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        List<T> Include(string includeTable);
        T Find(Expression<Func<T, bool>> filter);
        T FindFirst(Expression<Func<T, bool>> filter);
        T Inculde2(string includeTable);
        List<T> TolistInclude(params Expression<Func<T, object>>[] children);
    }
}
