using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DLL.Entities;
using DLL.Repositories;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CardRepository Cards { get; }
        CategoryRepository Categories { get; }
        UserRepository Users { get; }
        public Task SaveAsync();
    }
}
