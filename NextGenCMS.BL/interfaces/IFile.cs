using NextGenCMS.Model.Alfresco.File;
using NextGenCMS.Model.classes.File;
using System.IO;

namespace NextGenCMS.BL.interfaces
{
    public interface IFile
    {
        dynamic GetFiles(FilePath filePath);
        void  Download(string url, string fileName,string token);
        void Upload();
        FileRootObject DeleteFile(FilePath filePath);

    }
}
