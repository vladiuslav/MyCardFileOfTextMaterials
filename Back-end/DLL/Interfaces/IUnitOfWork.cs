using System;
using System.Collections.Generic;
using System.Text;
using DLL.Entities;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Card> Cards { get; }
        IRepository<Category> Categories { get; }
        IRepository<User> Users { get; }
        IRepository<Like> Likes { get; }
        void Save();
    }
}
