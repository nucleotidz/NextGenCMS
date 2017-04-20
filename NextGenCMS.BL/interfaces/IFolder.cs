﻿using NextGenCMS.Model.classes;
using NextGenCMS.Model.classes.Folder;
using System.Collections.Generic;

namespace NextGenCMS.BL.interfaces
{
    public interface IFolder
    {
        List<FolderModel> GetRootFolders();
        List<FolderModel> GetSubFoldersPath(string path);
        FolderModel CreateFolder(AddFolderModel folderModel);
        FolderModel CreateSubFolder(AddSubFolderModel folderModel);
    }
}
