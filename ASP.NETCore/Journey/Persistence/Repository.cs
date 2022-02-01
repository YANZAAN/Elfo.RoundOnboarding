using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Journey.Persistence
{
    public class Repository<T>
        where T : class
    {
        /// <summary>
        ///     Internal DbContext instance for changes manipulation & tracking
        /// </summary>
        private readonly DbContext _context;
        /// <summary>
        ///     Internal DbSet instance for data manipulation
        /// </summary>
        private readonly DbSet<T> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = default,
            Expression<Func<T, object>>[] includeProperties = default,
            Expression<Func<T, object>> orderBy = default)
        {
            IQueryable<T> query = _set;

            if (filter != default)
            {
                query = query.Where(filter);
            }

            if (includeProperties != default)
            {
                query = includeProperties.Aggregate(
                    query,
                    (current, includeProperty) => current.Include(includeProperty));
            }

            if (orderBy != default)
            {
                query = query.OrderBy(orderBy);
            }

            return query.ToList();
        }

        public T GetById(object id)
        {
            return _set.Find(id);
        }

        public void Insert(T entity)
        {
            _set.Add(entity);
        }

        public void Delete(object id)
        {
            T entityToDelete = _set.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _set.Attach(entityToDelete);
            }
            _set.Remove(entityToDelete);
        }

        public void Update(T entityToUpdate)
        {
            _set.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}