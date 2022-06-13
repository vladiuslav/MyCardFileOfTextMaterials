using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLL.Entities;
using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    class CategoryRepository : IRepository<Category>
    {
        private DataContext db;
        public CategoryRepository(DataContext dataContext)
        {
            this.db = dataContext;
        }
        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
                db.Categories.Remove(category);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
