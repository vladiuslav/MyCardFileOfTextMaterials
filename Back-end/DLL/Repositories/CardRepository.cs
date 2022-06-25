using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class CardRepository : RepositoryBase<Card>
    {
        public CardRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<IEnumerable<Card>> FindAsync(Expression<Func<Card, bool>> predicate)
        {
            return await Find(predicate).ToListAsync();
        }
        public async Task<Card> FirstOrDefaultAsync(Expression<Func<Card, bool>> predicate)
        {
            return await dataContext.Cards.FirstOrDefaultAsync(predicate);
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            return await Find(card => card.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public new void Create(Card item)
        {
            Create(item);
        }

        public new void Delete(Card item)
        {
            Delete(item);
        }
        public new void Update(Card item)
        {
            Update(item);
        }
    }
}
