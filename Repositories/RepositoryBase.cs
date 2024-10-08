using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        protected readonly RepositoryContext _context;
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ?
                _context.Set<T>() :
                _context.Set<T>().AsNoTracking();
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        => trackChanges
        ? _context.Set<T>().Where(expression).SingleOrDefault()
        : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}