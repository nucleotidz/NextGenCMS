using NextGenCMS.Model.Alfresco.Common;
using NextGenCMS.Model.classes;
using NextGenCMS.Model.classes.Folder;
using System.Collections.Generic;

namespace NextGenCMS.BL.interfaces
{
    public interface IFolderNext
    {
        List<FolderModel> GetRootFolders();
        List<FolderModel> GetSubFoldersPath(string path);
        FolderModel CreateFolder(AddFolderModel folderModel);
        FolderModel CreateSubFolder(AddSubFolderModel folderModel);
        void CheckOutFile(CheckoutParamsModel objParams);
        void CancelCheckout(string docId);
        void CheckIn(string docId);
        DeleteRootObject DeleteFolder(FolderPath folderPath);
    }
}
