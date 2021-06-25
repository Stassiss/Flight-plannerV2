using System;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity
    {
        protected IAppDbContext _dbContext;

        protected RepositoryBase(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ?
            _dbContext.Set<T>()
                .AsNoTracking() :
           _dbContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                _dbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                _dbContext.Set<T>()
                    .Where(expression);
        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    }
}
