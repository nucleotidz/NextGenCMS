using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.File;
using NextGenCMS.Model.constants;
using System;
using System.Dynamic;
using System.Web;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NextGenCMS.BL.classes
{
    public class File : IFile
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;

        public File(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
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

        public void Download(FilePath filePath)
        {
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
              this._apiHelper.Get(AppConfigKeys.ServiceUrl + "alfresco/s/" + filePath.path + "?a=true&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

           
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
