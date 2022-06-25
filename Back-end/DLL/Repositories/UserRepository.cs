using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await Find(predicate).ToListAsync();
        }
        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await dataContext.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await Find(user => user.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public new void Create(User item)
        {
            Create(item);
        }

        public new void Delete(User item)
        {
            Delete(item);
        }
        public new void Update(User item)
        {
            Update(item);
        }
    }
}
