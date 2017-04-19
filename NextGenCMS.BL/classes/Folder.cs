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

namespace NextGenCMS.BL.classes
{
    public class Folder : IFolder
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

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