using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;
using DLL.Interfaces;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataContext dataContext;
        private CardRepository cardRepository;
        private CategoryRepository categoryRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(string connectionString)
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(connectionString)
            .Options;
            dataContext = new DataContext(contextOptions);
        }
        public CardRepository Cards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(dataContext);
                return cardRepository;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(dataContext);
                return categoryRepository;
            }
        }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(dataContext);
                return userRepository;
            }
        }

        public async Task SaveAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
