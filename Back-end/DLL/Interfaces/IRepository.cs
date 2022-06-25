using System;
using System.Linq;
using System.Linq.Expressions;

namespace DLL.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}
