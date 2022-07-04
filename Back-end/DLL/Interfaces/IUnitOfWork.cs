using DLL.Entities;
using System;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Card> Cards { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
