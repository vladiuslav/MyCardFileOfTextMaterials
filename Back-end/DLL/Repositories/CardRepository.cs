﻿using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.Repositories
{
    public class CardRepository : IRepository<Card>
    {
        private DataContext db;
        public CardRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public void Create(Card item)
        {
            db.Cards.Add(item);
        }

        public void Delete(int id)
        {
            Card card = db.Cards.Find(id);
            if (card != null)
                db.Cards.Remove(card);
        }

        public IEnumerable<Card> Find(Func<Card, bool> predicate)
        {
            return db.Cards.Where(predicate).ToList();
        }

        public Card FirstOrDefault(Func<Card, bool> predicate)
        {
            return db.Cards.FirstOrDefault(predicate);
        }

        public Card Get(int id)
        {
            return db.Cards.Find(id);
        }

        public IEnumerable<Card> GetAll()
        {
            return db.Cards;
        }

        public void Update(Card item)
        {
            var card = db.Cards.FirstOrDefault(card => card.Id == item.Id);
            card.Text = item.Text;
            card.Title = item.Title;
        }
    }
}
