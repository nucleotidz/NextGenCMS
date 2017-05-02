using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.Search;
using NextGenCMS.Model.constants;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NextGenCMS.BL.classes
{
    public class SearchBL : ISearchBL
    {

        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;


        public SearchBL(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
        }

        public dynamic SearchFile(string searchKey, bool IsContent)
        {
            string data = string.Empty;
            string termKey = string.Empty;
            string query = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                if (!IsContent)
                {
                    query = "{\"prop_cm_name\":" + "\"*" + searchKey + "*\",\"datatype\":\"cm:content\"}";
                }
                else
                {                
                    termKey = searchKey;                  
                }
                data = this._apiHelper.Get(ServiceUrl.SearchfileURL + "&term=" + termKey + "&query=" + query + ServiceUrl.searchQuerystring + "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            var converter = new ExpandoObjectConverter();
            dynamic dataObject = JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
            return dataObject;
        }


    }
}
