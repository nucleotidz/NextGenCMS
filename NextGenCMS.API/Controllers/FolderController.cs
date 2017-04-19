using NextGenCMS.API.Filters;
using NextGenCMS.BL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NextGenCMS.Model.Alfresco.Folder;
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
            
            return Request.CreateResponse(HttpStatusCode.OK,_folder.GetRootFolders());
        }
    }
}
