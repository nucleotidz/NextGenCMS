

namespace NextGenCMS.UnitOfWork.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;


    /// <summary>
    /// Generic Repository Inteface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Get all the entities from the db context.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="includeProperties">includeProperties</param>
        /// <returns> List of TEntity </returns>
        IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        /// <summary>
        /// GetQuery
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="includeProperties">includeProperties</param>
        /// <returns>TEntity of IQueryable</returns>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Method to get list of entities
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <param name="orderBy">Oerdering clause.</param>
        /// <param name="includeProperties">Properties to be included.</param>
        /// <returns>A list of entities.</returns>
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Get a single entity by primary key.
        /// </summary>
        /// <param name="id">record id</param>
        /// <returns>TEntity</returns>
        TEntity GetById(object id);

        //void GetCloneById(object id);

        /// <summary>
        /// Insert entity to db context.
        /// </summary>
        /// <param name="entity">entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update entity in db
        /// </summary>
        /// <param name="entity">entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity from db by primary key.
        /// </summary>
        /// <param name="id">record id</param>
        void Delete(object id);

        /// <summary>
        /// Delete entities from db.
        /// </summary>
        /// <param name="entities">entities</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <typeparam name="T">Type of the instance</typeparam>
        /// <returns>Iqueryable instance</returns>
        IQueryable<T> Query<T>() where T : class;
    }
}