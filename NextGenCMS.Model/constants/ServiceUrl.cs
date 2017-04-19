using System.IO;
using System.Web;

namespace NextGenCMS.Model.constants
{
    public class ServiceUrl
    {
        /// <summary>
        /// string x-thirdparty-Id 
        /// </summary>
        public static readonly string Login = AppConfigKeys.ServiceUrl + "/alfresco/s/api/login";
        public static readonly string Folder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/datalists/lists/site/" + AppConfigKeys.Site + "/documentLibrary?alf_ticket=";
    }
}
