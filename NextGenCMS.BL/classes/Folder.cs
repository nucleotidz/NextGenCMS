using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NextGenCMS.Model.Alfresco.Folder;
using System.Web;
using NextGenCMS.Model.classes;
using NextGenCMS.Model.classes.Folder;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using DotCMIS.Client.Impl;
using DotCMIS;
using DotCMIS.Client;
using NextGenCMS.Model.Alfresco.Common;

namespace NextGenCMS.BL.classes
{
    public class Folder : IFolderNext
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        private ISession session = null;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;



        public Folder(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
        }

        public List<FolderModel> GetRootFolders()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.Folder + HttpContext.Current.Items[Filter.Token]);
            }

            RootObject dataObject = JsonConvert.DeserializeObject<RootObject>(data);
            return this.MapFolder(dataObject.datalists);
        }

        public List<FolderModel> GetSubFoldersPath(string path)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.SubFolder + path + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            SRootObject dataObject = JsonConvert.DeserializeObject<SRootObject>(data);
            return this.MapSubFolder(dataObject.items);
        }

        public FolderModel CreateFolder(AddFolderModel folderModel)
        {
            folderModel.type = FileFolder.type;
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Post(ServiceUrl.AddFolder + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(folderModel));
            }
            AddFolderRootObject dataObject = JsonConvert.DeserializeObject<AddFolderRootObject>(data);
            return new FolderModel
            {
                Name = folderModel.name,
                Noderef = dataObject.nodeRef,
                Title = folderModel.title,
                HasChildren = false
            };
        }

        public FolderModel CreateSubFolder(AddSubFolderModel folderModel)
        {
            folderModel.folder.type = FileFolder.type;
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Post(ServiceUrl.AddFolder + folderModel.path + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(folderModel.folder));
            }
            AddFolderRootObject dataObject = JsonConvert.DeserializeObject<AddFolderRootObject>(data);
            return new FolderModel
            {
                Name = folderModel.folder.name,
                Noderef = dataObject.nodeRef,
                Title = folderModel.folder.title,
                HasChildren = false
            };
        }

        public DeleteRootObject DeleteFolder(FolderPath folderPath)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Delete(ServiceUrl.DeleteFolder + folderPath.path + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<DeleteRootObject>(data);
           
        }
        public void CheckOutFile(CheckoutParamsModel objParams)
        {
            this.session = this.GetSession();
            Document doc = (Document)this.session.GetObjectByPath("/sites/" + AppConfigKeys.Site + "/documentLibrary/" + objParams.path);
            String fileName = doc.ContentStreamFileName;
            IObjectId pwcId = doc.CheckOut();
        }

        public void CancelCheckout(string docId)
        {
            this.session = this.GetSession();
            IObjectId obj = new ObjectId(docId);
            IDocument pwc = (IDocument)this.session.GetObject(obj);
            pwc.CancelCheckOut();
        }

        public void CheckIn(string docId)
        {
            this.session = this.GetSession();
            IObjectId obj = new ObjectId(docId);
            IDocument pwc = (IDocument)this.session.GetObject(obj);
            Dictionary<string, Object> properties = new Dictionary<string, object>();
            properties["cmis:description"] = "New change";
            var contentStream = session.GetContentStream(obj);
            string checkinComment = "test change";
            IObjectId newId = pwc.CheckIn(false, properties, contentStream, checkinComment);           
        }

        private List<FolderModel> MapFolder(List<Datalist> dataObject)
        {
            List<FolderModel> model = new List<FolderModel>();
            dataObject.ForEach(x =>
            {
                model.Add(new FolderModel
                {
                    HasChildren = true,
                    Name = x.name,
                    Title = x.title,
                    Noderef = x.nodeRef
                });
            });
            return model;
        }



        private List<FolderModel> MapSubFolder(List<Item> dataObject)
        {
            List<FolderModel> model = new List<FolderModel>();
            dataObject.ForEach(x =>
            {
                model.Add(new FolderModel
                {
                    HasChildren = x.hasChildren,
                    Name = x.name,
                    Title = x.name,
                    Noderef = x.nodeRef
                });
            });
            return model;
        }

        private ISession GetSession()
        {
            if (session == null)
            {
                SessionFactory factory = SessionFactory.NewInstance();
                Dictionary<String, String> parameter = new Dictionary<String, String>();
                parameter.Add(SessionParameter.User, "admin");
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

                }
            }

            this._disposed = true;
        }
        #endregion
    }
}