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

        public List<Datalist> GetRootFolders()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.Folder + HttpContext.Current.Items[Filter.Token]);
            }
          
            RootObject dataObject = JsonConvert.DeserializeObject<RootObject>(data);
            return dataObject.datalists;
        }

        public void GetSubFoldersPath(string path)
        {

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