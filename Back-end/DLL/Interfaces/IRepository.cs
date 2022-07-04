using System;
using System.Collections.Generic;

namespace DLL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        T FirstOrDefault(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
