using NextGenCMS.UnitOfWork.Interfaces;
using System;

namespace NextGenCMS.DL.interfaces
{
    public interface IFileRepository : IBaseRepository, IDisposable
    {
        void SaveMeta(string name);
    }
}
