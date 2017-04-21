using NextGenCMS.API.Filters;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace NextGenCMS.API.Controllers
{
    [RoutePrefix("api/File")]
    [Secure]
    public class FileController : ApiController
    {
        IFile _file;
        public FileController(IFile file)
        {
            this._file = file;
        }

        [Route("Get")]
        [HttpPost]
        public HttpResponseMessage GetFiles(FilePath filePath)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)_file.GetFiles(filePath));
        }
        [Route("Download")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Download()
        {
          _file.Download(HttpContext.Current.Request.Form[0], HttpContext.Current.Request.Form[1] ,HttpContext.Current.Request.Form[2]);           
          return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
