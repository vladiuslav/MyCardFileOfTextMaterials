using DLL.Entities;
using System;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    /// <summary>
    /// Intarface of Unit of work pattern, include repositories for work with db.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Card> Cards { get; }
        IRepository<Like> Likes { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        Task<int> SaveAsync();
    }
}
