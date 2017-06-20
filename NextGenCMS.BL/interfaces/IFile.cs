using NextGenCMS.Model.Alfresco.Common;
using NextGenCMS.Model.classes.File;
using System.Collections.Generic;

namespace NextGenCMS.BL.interfaces
{
    public interface IFile
    {
        dynamic GetFiles(FilePath filePath);
        void  Download(string url, string fileName,string token);
        void Upload();
        DeleteRootObject DeleteFile(FilePath filePath);
        void Download(string docId);
        List<object> GetVesrion(string nodeRef);
    }
}
