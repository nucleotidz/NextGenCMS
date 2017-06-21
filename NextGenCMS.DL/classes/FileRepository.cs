using NextGenCMS.DL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextGenCMS.UnitOfWork.Classes;
using NextGenCMS.UnitOfWork.Interfaces;
using NextGenCMS.ORM;

namespace NextGenCMS.DL.classes
{
    public class FileRepository : BaseRepository,IFileRepository
    {
        private bool _disposed;

        /// <summary>
        /// _unitOfWork IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<FileMeta_T> _repository;

        public FileRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = this._unitOfWork.GetRepository<FileMeta_T>();
           
        }

        public void SaveMeta(string name)
        {
            FileMeta_T file = new FileMeta_T();
            file.Name = name;
            _repository.Insert(file);
        }
        public void Dispose()
        {
            this.Dispose(true);
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
                    this._unitOfWork.Dispose();
                }
            }

            this._disposed = true;
        }
    }
}
