using DLL.Entities;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    /// <summary>
    /// Repository for working with table users.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private DataContext db;
        /// <summary>
        /// Simple constructor with data context
        /// </summary>
        /// <param name="dataContext">Data context for working with bd</param>
        public UserRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public async Task Create(User item)
        {
            await db.Users.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            User item = await db.Users.FindAsync(id);
            db.Users.Remove(item);
        }

        public async Task<User> GetAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public IQueryable<User> GetAll()
        {
            return db.Users.AsQueryable();
        }

        public void Update(User item)
        {
            var user = db.Users.FirstOrDefault(user => user.Id == item.Id);
            user.Email = item.Email;
            user.NickName = item.NickName;
            user.Password = item.Password;
        }
    }
}
