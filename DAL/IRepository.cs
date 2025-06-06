using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll(Func<T, bool> predicate);

        void Add(T entity);                                  
        void Update(T entity);                               
        void Remove(T entity);                                 
    }

}
