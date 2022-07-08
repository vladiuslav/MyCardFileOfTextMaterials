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
    /// Repository for working with table categories.
    /// </summary>
    public class CategoryRepository : IRepository<Category>
    {
        private DataContext db;
        /// <summary>
        /// Simple constructor with data context
        /// </summary>
        /// <param name="dataContext">Data context for working with bd</param>
        public CategoryRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public async Task Create(Category item)
        {
            await db.Categories.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            Category item = await db.Categories.FindAsync(id);
            db.Categories.Remove(item);
        }

        public async Task<Category> GetAsync(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public IQueryable<Category> GetAll()
        {
            return db.Categories.AsQueryable();
        }

        public void Update(Category item)
        {
            var category = db.Categories.FirstOrDefault(category => category.Id == item.Id);
            category.Name = item.Name;
        }
    }
}
