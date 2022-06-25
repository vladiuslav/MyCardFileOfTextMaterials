using DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DLL.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected DataContext dataContext;
        public RepositoryBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IQueryable<T> GetAll()
        {
            return this.dataContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.dataContext.Set<T>()
            .Where(predicate).AsNoTracking();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.dataContext.Set<T>().FirstOrDefault(predicate);
        }
        public void Create(T item)
        {
            this.dataContext.Set<T>().Add(item);
        }
        public void Update(T item)
        {
            this.dataContext.Set<T>().Update(item);
        }
        public void Delete(T item)
        {
            this.dataContext.Set<T>().Remove(item);
        }
        
    }
}
