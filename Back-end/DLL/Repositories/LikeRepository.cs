using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLL.Entities;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class LikeRepository : IRepository<Like>
    {
        private DataContext db;
        public LikeRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public void Create(Like item)
        {
            db.Likes.Add(item);
        }

        public void Delete(int id)
        {
            Like like = db.Likes.Find(id);
            if (like != null)
                db.Likes.Remove(like);
        }

        public IEnumerable<Like> Find(Func<Like, bool> predicate)
        {
            return db.Likes.Where(predicate).ToList();
        }

        public Like Get(int id)
        {
            return db.Likes.Find(id);
        }

        public IEnumerable<Like> GetAll()
        {
            return db.Likes;
        }

        public void Update(Like item)
        {
            var like = db.Likes.FirstOrDefault(like => like.Id == item.Id);
            like.UserId = item.UserId;
            like.CardId = item.CardId;
        }
    }
}
