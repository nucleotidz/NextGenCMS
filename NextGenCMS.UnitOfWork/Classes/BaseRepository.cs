using NextGenCMS.UnitOfWork.Interfaces;

namespace NextGenCMS.UnitOfWork.Classes
{
    /// <summary>
    ///  Description : BaseRepository is base class for all repository to expose commit functionality
    /// </summary>
    public abstract class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private IUnitOfWork _unitOfWork = null;

        /// <summary>
        /// Base Class Contructor
        /// </summary>
        /// <param name="_unitOfWork">unit of work</param>
        public BaseRepository(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Commits changes on the unit of work
        /// </summary>
        /// <returns>No of rows effected</returns>
        public int Commit()
        {
            return _unitOfWork.Commit();
        }
    }
}
