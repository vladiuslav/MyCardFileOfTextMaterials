using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    /// <summary>
    ///  Intarface with base repository function (CRED): create,read,edit,delete
    /// </summary>
    /// <typeparam name="T">Entity for repository inheritance IEntity</typeparam>
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
}
