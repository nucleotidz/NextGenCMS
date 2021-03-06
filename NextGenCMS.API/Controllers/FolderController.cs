﻿using NextGenCMS.API.Filters;
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
using NextGenCMS.Model.Alfresco.Common;
namespace NextGenCMS.API.Controllers
{
    [RoutePrefix("api/Folder")]
    [Secure]
    public class FolderController : ApiController
    {
        IFolderNext _folder;
        public FolderController(IFolderNext folder)
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

        [HttpPost]
        [Route("Checkout/File")]
        public HttpResponseMessage CheckOutFile(CheckoutParamsModel objPath)
        {
            _folder.CheckOutFile(objPath);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("CancelCheckout/File/{objectId}")]
        public HttpResponseMessage CancelCheckOut(string objectId)
        {
            _folder.CancelCheckout(objectId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("CheckIn/File/{objectId}")]
        public HttpResponseMessage CheckInFile(string objectId)
        {
            _folder.CheckIn(objectId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Delete")]
        public HttpResponseMessage DeleteFolder(FolderPath folderPath)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.DeleteFolder(folderPath));
        }

        [HttpGet]
        [Route("CheckOutCount/{userName}")]
        public HttpResponseMessage GetCheckedOutCount(string userName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _folder.CheckOutCountbyUser(userName));
        }
    }
}
