using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
}
