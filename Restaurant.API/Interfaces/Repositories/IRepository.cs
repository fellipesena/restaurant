﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Restaurant.API.Context.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entitiy);
        void Remove(TEntity entity);
    }
}
