using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private DataContext db;
        public UserRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User FirstOrDefault(Func<User, bool> predicate)
        {
            return db.Users.FirstOrDefault(predicate);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
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
