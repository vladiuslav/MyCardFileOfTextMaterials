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
    public class CardRepository : IRepository<Card>
    {
        private DataContext db;
        public CardRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public async Task Create(Card item)
        {
            await db.Cards.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            Card item = await db.Cards.FindAsync(id);
            db.Cards.Remove(item);
        }

        public async Task<Card> GetAsync(int id)
        {
            var card = await db.Cards.FindAsync(id);
            card.Likes = await db.Likes.AsQueryable().Where(like => like.CardId == card.Id).ToListAsync();
            return card;
        }

        public IQueryable<Card> GetAll()
        {
            return db.Cards.AsQueryable().Include(c=>c.Likes);
        }

        public void Update(Card item)
        {
            var card = db.Cards.FirstOrDefault(card => card.Id == item.Id);
            card.Text = item.Text;
            card.Title = item.Title;
        }
    }
}
