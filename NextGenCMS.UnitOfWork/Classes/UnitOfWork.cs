using NextGenCMS.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.UnitOfWork.Classes
{
    /// <summary>
    /// Generic class to handle multiple local transactions as a single unit of work.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext, new()
    {
        /// <summary>
        /// private variable
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// DbContext object
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// object of Dictionary of Type and Object
        /// </summary>
        private Dictionary<Type, object> _repositories;
      

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}" /> class.
        /// </summary>
        /// <param name="TContext">The type of the entity.</param>       
        public UnitOfWork(TContext context)
        {
            _disposed = false;
            _dbContext = context;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Returns db context object from unit of work
        /// </summary>
        /// <returns>db Context</returns>
        public DbContext GetDbContext()
        {
            return this._dbContext;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>IRepository of the Entity</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            // Checks if the Dictionary Key contains the Model class
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                // Return the repository for that Model class
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            // If the repository for that Model class doesn't exist, create it
            var repository = new Repository<TEntity>(_dbContext);

            // Add it to the dictionary
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>integer</returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                this._disposed = true;
            }
        }
    }
}
