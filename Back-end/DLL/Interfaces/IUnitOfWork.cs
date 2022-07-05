using DLL.Entities;
using System;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Card> Cards { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        Task<int> SaveAsync();
    }
}
