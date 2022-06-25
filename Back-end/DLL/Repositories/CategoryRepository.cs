using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            return await Find(predicate).ToListAsync();
        }
        public async Task<Category> FirstOrDefaultAsync(Expression<Func<Category, bool>> predicate)
        {
            return await dataContext.Categories.FirstOrDefaultAsync(predicate);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await Find(category => category.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        public new void Create(Category item)
        {
            Create(item);
        }

        public new void Delete(Category item)
        {
            Delete(item);
        }
        public new void Update(Category item)
        {
            Update(item);
        }
    }
}
