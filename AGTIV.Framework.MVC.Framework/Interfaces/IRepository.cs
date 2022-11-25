using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AGTIV.Framework.MVC.Entities.Shared;

namespace AGTIV.Framework.MVC.Framework.Interfaces
{
    public interface IRepository
    {
        IEnumerable<TEntity> Get<TEntity>(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    string includeProperties = "")
                    where TEntity : class;

        TEntity GetByID<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets filtered records of entity from the repository.
        /// </summary>
        /// <returns>Filtered records in <see cref="IEnumerable{T}"/> of <typeparamref cref="TEntity"/> object.</returns>
        List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets single entity from the repository.
        /// </summary>
        /// <returns>Single records in <see cref="IEnumerable{T}"/> of <typeparamref cref="TEntity"/> object.</returns>
        TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets filtered records of entity from the repository without tracking.
        /// </summary>
        /// <returns>Filtered records in <see cref="IEnumerable{T}"/> of <typeparamref cref="TEntity"/> object without tracking.</returns>
        List<TEntity> GetWithoutTrack<TEntity>(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets all records of entity from the repository.
        /// </summary>
        /// <returns>All records in <see cref="IEnumerable{T}"/> of <typeparamref cref="TEntity"/> object.</returns>
        List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        /// <summary>
        /// Gets query.
        /// </summary>
        /// <returns>Query</returns>
        IQueryable<TEntity> GetQuery<TEntity>(bool isTracking, Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity : class, IEntity;

        void Insert<TEntity>(TEntity entity)
            where TEntity : class;

        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entityToDelete)
            where TEntity : class;

        void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : class;

        void Attach<TEntity>(TEntity entity)
            where TEntity : class;

        void Reload<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, new();
    }
}
