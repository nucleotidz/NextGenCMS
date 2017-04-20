
namespace NextGenCMS.DL.classes
{
    #region Namespaces
    using System;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.DL.interfaces;
    #endregion

    public class AdministrationRepository : IAdministrationRepository
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        #region Dispose
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overriding Dispose method
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
