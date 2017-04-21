using NextGenCMS.Model.classes.File;

namespace NextGenCMS.BL.interfaces
{
    public interface IFile
    {
        dynamic GetFiles(FilePath filePath);
        void Download(FilePath filePath);
    }
}
