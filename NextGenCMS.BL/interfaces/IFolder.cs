using NextGenCMS.Model.Alfresco.Folder;
using NextGenCMS.Model.classes;
using System.Collections.Generic;

namespace NextGenCMS.BL.interfaces
{
    public interface IFolder
    {
        List<FolderModel> GetRootFolders();
        List<FolderModel> GetSubFoldersPath(string path);
    }
}
