using NextGenCMS.Model.Alfresco.Folder;
using System.Collections.Generic;

namespace NextGenCMS.BL.interfaces
{
    public interface IFolder
    {
        List<Datalist> GetRootFolders();
        void GetSubFoldersPath(string path);
    }
}
