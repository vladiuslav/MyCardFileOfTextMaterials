using DLL.Entities;
using DLL.Interfaces;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DLL
{
    /// <summary>
    /// Unit of work is used for working with repositories.
    /// Repositories used for working with specific tables.
    /// Have methods for dispose and async save of data base.
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private DataContext db;
        private CardRepository cardRepository;
        private CategoryRepository categoryRepository;
        private UserRepository userRepository;
        private LikeRepository likeRepository;
        /// <summary>
        /// Constructor is used for creating context of db, using Microsoft sql server. 
        /// </summary>
        /// <param name="connectionString">Connection string for options</param>
        public EFUnitOfWork(string connectionString)
        {
            db = new DataContext(connectionString);
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

        public IRepository<Like> Likes
        {
            get
            {
                if (likeRepository == null)
                    likeRepository = new LikeRepository(db);
                return likeRepository;
            }
        }
        public Task<int> SaveAsync()
        {
            return db.SaveChangesAsync();
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
