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
        public static readonly string SubFolder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/treenode/site/" + AppConfigKeys.Site + "/documentLibrary/";
    }
}
