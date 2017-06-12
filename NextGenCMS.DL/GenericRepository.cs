using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace NextGenCMS.DL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        
        /// <summary>
        /// DBcontext object
        /// </summary>
        private DbContext _dbContext;

        /// <summary>
        /// object of DbSet of TEntity
        /// </summary>
        private DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
        /// </summary>
        /// <param name="dbContext">The context.</param>
        public GenericRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets list of entities by the specified filter.
        /// Filters are optional.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List of The entity</returns>
        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        /// <summary>
        /// Method to get list of entities
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <param name="orderBy">Oerdering clause.</param>
        /// <param name="includeProperties">Properties to be included.</param>
        /// <returns>A list of entities.</returns>
        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        /// <summary>
        /// Gets list of entities by the specified filter.
        /// Filters are optional.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>List of The entity</returns>
        public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        /// <summary>
        /// Gets a single entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity</returns>
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }
      
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            MarkPropertiesDirty(entity);
        }
        /// <summary>
        /// As in the parent function we have marked entire entity as dirty, this function Marks the required properties as dirty. Which will be sent to database for updation
        /// </summary>
        /// <param name="entity">The entity.</param>
        private void MarkPropertiesDirty(TEntity entity)
        {
            DbPropertyValues original;
            ObjectContext _objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
            List<DbEntityEntry> changeList = _dbContext.ChangeTracker.Entries().ToList().Where(x => x.State == EntityState.Modified).ToList();
            foreach (var changeEntry in changeList)
            {
                List<string> PKs = GetPrimaryKeyValue(changeEntry);
                ObjectStateEntry stateEntry = _objectContext.ObjectStateManager.GetObjectStateEntry(changeEntry.Entity);
                List<string> ModifiedProperties = stateEntry.GetModifiedProperties().ToList();
                original = changeEntry.GetDatabaseValues();
                if (ModifiedProperties != null)
                {
                    foreach (string propertyName in ModifiedProperties)
                    {
                        original.GetValue<object>(propertyName);
                        object newValueObject = changeEntry.CurrentValues.GetValue<object>(propertyName);
                        object oldValueObject = original.GetValue<object>(propertyName);
                        string newValueString = Convert.ToString(newValueObject);
                        string oldValueString = Convert.ToString(oldValueObject);
                        if (oldValueObject != null && newValueObject != null && oldValueObject.GetType() == typeof(byte[])
                                                       && newValueObject.GetType() == typeof(byte[]))
                        {
                            continue;
                        }
                        else if (oldValueString == newValueString)
                        {
                            if (!PKs.Contains(propertyName))
                                _dbContext.Entry(entity).Property(propertyName).IsModified = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the primary key value.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>list of props</returns>
        private List<string> GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues.Select(q => q.Key).ToList();
        }

        /// <summary>
        /// Deletes the entity by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                _dbSet.Attach(item);
            }
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <typeparam name="T"> The type to be queried</typeparam>
        /// <returns>An Iqueryable instance of the queried type.</returns>
        public virtual IQueryable<T> Query<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
