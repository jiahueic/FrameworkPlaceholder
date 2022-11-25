using AGTIV.Framework.MVC.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AGTIV.Framework.MVC.Entities.Shared;

namespace AGTIV.Framework.MVC.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly DbContext _context;

        public Repository(
            DbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
            where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetByID<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            if(includeProperties != null)
            {
                IQueryable<TEntity> query = GetAllIncluding(true, includeProperties);
                return query.Where(c => c.Id == new Guid(id.ToString())).SingleOrDefault();
            }

            return _context.Set<TEntity>().Find(id);
        }

        public List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQuery<TEntity>(true, filter, includeProperties).ToList();
        }

        public TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQuery<TEntity>(true, filter, includeProperties).SingleOrDefault();
        }

        public List<TEntity> GetWithoutTrack<TEntity>(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            return GetQuery<TEntity>(false, filter, includeProperties).ToList();
        }

        public List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = GetAllIncluding(true, includeProperties);

            return query.ToList();
        }

        public IQueryable<TEntity> GetQuery<TEntity>(bool isTracking, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = GetAllIncluding(isTracking, includeProperties);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public void Insert<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(object id)
            where TEntity : class
        {
            TEntity entityToDelete = _context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        public void Delete<TEntity>(TEntity entityToDelete)
            where TEntity : class
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }
            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : class
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        private IQueryable<TEntity> GetAllIncluding<TEntity>(bool isTracking, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (!isTracking)
                queryable = queryable.AsNoTracking();

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public void Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Reload<TEntity>(TEntity entity) 
            where TEntity : class, IEntity
        {
            _context.Entry(entity).Reload();   
        }

        #region Imported Entity
        public IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, new()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
        #endregion
    }
}
