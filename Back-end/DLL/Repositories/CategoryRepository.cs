﻿using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.Repositories
{
    public class CategoryRepository : IRepository<Category>
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

        public Category FirstOrDefault(Func<Category, bool> predicate)
        {
            return db.Categories.FirstOrDefault(predicate);
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
            var category = db.Categories.FirstOrDefault(category => category.Id == item.Id);
            category.Name = item.Name;
        }
    }
}
