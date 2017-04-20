using NextGenCMS.API.Filters;
using NextGenCMS.BL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NextGenCMS.Model.Alfresco.Folder;
using NextGenCMS.Model.classes;
using NextGenCMS.Model.classes.Folder;
namespace NextGenCMS.API.Controllers
{
    [RoutePrefix("api/Folder")]
    [Secure]
    public class FolderController : ApiController
    {
        IFolder _folder;
        public FolderController(IFolder folder)
        {
            this._folder = folder;
        }

        [Route("Get")]
        public HttpResponseMessage GetRootFolders()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.GetRootFolders());
        }

        [HttpPost]
        [Route("Get/SubFolder")]
        public HttpResponseMessage GetSubFolderFolders(SubFolderModel subFolderModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.GetSubFoldersPath(subFolderModel.path));
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage CreateFolder(AddFolderModel folderModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.CreateFolder(folderModel));
        }

        [HttpPost]
        [Route("Create/SubFolder")]
        public HttpResponseMessage CreateSubFolder(AddSubFolderModel folderModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.CreateSubFolder(folderModel));
        }
    }
}
