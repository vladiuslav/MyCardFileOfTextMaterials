using DLL.Entities;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    /// <summary>
    /// Repository for working with table likes.
    /// </summary>
    class LikeRepository : IRepository<Like>
    {
        private DataContext db;
        /// <summary>
        /// Simple constructor with data context
        /// </summary>
        /// <param name="dataContext">Data context for working with bd</param>

        public LikeRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public async Task Create(Like item)
        {
            await db.Likes.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            Like item = await db.Likes.FindAsync(id);
            db.Likes.Remove(item);
        }

        public IQueryable<Like> GetAll()
        {
            return db.Likes.AsQueryable();
        }

        public async Task<Like> GetAsync(int id)
        {
            return await db.Likes.FindAsync(id);
        }

        public void Update(Like item)
        {
            var like = db.Likes.FirstOrDefault(like => like.Id == item.Id);
            like.IsDislike = item.IsDislike;
        }
    }
}
