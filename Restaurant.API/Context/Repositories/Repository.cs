using Microsoft.EntityFrameworkCore;
using Restaurant.API.Context.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurant.API.Context.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context) => Context = context;

        public TEntity Get(int id) => Context.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => Context.Set<TEntity>().Where(predicate);

        public void Add(TEntity entity)
        {
            _ = Context.Set<TEntity>().Add(entity);
            _ = Context.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            _ = Context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            _ = Context.SaveChanges();
        }
    }
}
