using System;
using System.Collections.Generic;
using System.Text;
using DLL.Entities;
using DLL.Interfaces;
using DLL.Repositories;


namespace DLL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataContext db;
        private CardRepository cardRepository;
        private CategoryRepository categoryRepository;
        private UserRepository userRepository;

        public EFUnitOfWork()
        {
            db = new DataContext();
        }
        public EFUnitOfWork(DataContext dataContext)
        {
            db = dataContext;
        }
        public IRepository<Card> Cards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(db);
                return cardRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
