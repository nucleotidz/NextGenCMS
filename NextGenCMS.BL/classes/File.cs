﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.File;
using NextGenCMS.Model.constants;
using System;
using System.Dynamic;
using System.Web;
using DotCMIS.Client.Impl;
using System.Collections.Generic;
using DotCMIS.Client;
using DotCMIS;
using DotCMIS.Data.Impl;
using NextGenCMS.Model.Alfresco.Common;
using System.IO;
using NextGenCMS.DL.interfaces;

namespace NextGenCMS.BL.classes
{
    public class File : IFile
    {
        private ISession session = null;

        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;
        private readonly IFileRepository _fileRepository;
        public File(IAPIHelper apiHelper,IFileRepository fileRepository)
        {
            this._apiHelper = apiHelper;
            this._fileRepository = fileRepository;
        }

        public List<object> GetVesrion(string nodeRef)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.GetVersion + nodeRef+ "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            List<object> dataObject = JsonConvert.DeserializeObject<List<object>>(data);
            return dataObject;
        }
        public dynamic GetFiles(FilePath filePath)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.File + filePath.path + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            var converter = new ExpandoObjectConverter();
            dynamic dataObject = JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
            return dataObject;
        }

        public void Download(string url, string fileName, string token)
        {
            this._apiHelper.DownLoad(ServiceUrl.FileDownload + url + "?a=true&alf_ticket=" + token, fileName);
        }
        public DeleteRootObject DeleteFile(FilePath filePath)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Delete(ServiceUrl.DeleteFile + filePath.path + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<DeleteRootObject>(data);
        }
        public void Upload()
        {
            this.session = this.GetSession();
            IFolder folder = (IFolder)this.session.GetObjectByPath("/sites/" + AppConfigKeys.Site + "/documentLibrary/" + HttpContext.Current.Request.Form["path"] + "/");
            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                string formattedName = HttpContext.Current.Request.Files[i].FileName;
                IDictionary<string, object> properties = new Dictionary<string, object>();
                properties.Add(PropertyIds.Name, formattedName);
                properties.Add(PropertyIds.ObjectTypeId, "cmis:document");
                string mime = System.Web.MimeMapping.GetMimeMapping(formattedName);
                ContentStream contentStream = new ContentStream
                {
                    FileName = formattedName,
                    MimeType = mime,
                    Length = 100,
                    Stream = HttpContext.Current.Request.Files[i].InputStream
                };
                IDocument doc = folder.CreateDocument(properties, contentStream, null);
               // _fileRepository.SaveMeta(doc.VersionSeriesId);
              //  _fileRepository.Commit();
            }
        }

        public void Download(string docId)
        {

            this.session = this.GetSession();
            IObjectId obj = this.session.CreateObjectId(docId);
            IDocument doc = (IDocument)this.session.GetObject(obj);
            var contentStream = doc.GetContentStream();
            Stream fileStream = contentStream.Stream;
            MemoryStream ms = new MemoryStream();
            fileStream.CopyTo(ms);
            byte[] response = ms.ToArray();
            ms.Dispose();
            HttpContext.Current.Response.ContentType = contentStream.MimeType;
            string header = string.Format("attachment;filename=" + contentStream.FileName);
            HttpContext.Current.Response.AddHeader("Content-Disposition", header);
            HttpContext.Current.Response.OutputStream.Write(response, 0, response.Length);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }

        private ISession GetSession()
        {
            if (session == null)
            {  
                string username = "admin";
                if (HttpContext.Current.Request.Form != null && HttpContext.Current.Request.Form["tenant"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Form["tenant"].ToString()))
                {
                    username = username + "@" + HttpContext.Current.Request.Form["tenant"].ToString();
                }
                SessionFactory factory = SessionFactory.NewInstance();
                Dictionary<String, String> parameter = new Dictionary<String, String>();
                parameter.Add(SessionParameter.User, username);
                parameter.Add(SessionParameter.Password, "admin");
                parameter.Add(SessionParameter.AtomPubUrl, ServiceUrl.CMISApi);
                parameter.Add(SessionParameter.BindingType, BindingType.AtomPub);
                this.session = factory.GetRepositories(parameter)[0].CreateSession();
            }
            return this.session;
        }

        #region Dispose
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overriding Dispose method
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._fileRepository.Dispose();
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
