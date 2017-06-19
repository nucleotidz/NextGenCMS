
namespace NextGenCMS.UnitOfWork.Interfaces
{
    using System;
    using System.Data.Entity;
    /// <summary>
    /// Generic interface to handle multiple local transaction as a single unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>IRepository object of TEntity class</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>int</returns>
        int Commit();

        /// <summary>
        /// Returns db context object from unit of work
        /// </summary>
        /// <returns>db Context</returns>
        DbContext GetDbContext();
    }

}
