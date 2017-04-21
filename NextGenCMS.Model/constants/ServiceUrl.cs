using System.IO;
using System.Web;

namespace NextGenCMS.Model.constants
{
    public class ServiceUrl
    {
        #region "Authentication"
        /// <summary>
        /// POST - This will is used to login and returns ticket
        /// </summary>
        public static readonly string Login = AppConfigKeys.ServiceUrl + "/alfresco/s/api/login";

        /// <summary>
        /// DELETE - This api will delete the ticket and logout the user
        /// </summary>
        public static readonly string Logout = AppConfigKeys.ServiceUrl + "/alfresco/s/api/login/ticket/";
        #endregion

        #region "Administration - User"
        /// <summary>
        /// POST - This api will create a new user and returns user details
        /// </summary>
        public static readonly string CreateUser = AppConfigKeys.ServiceUrl + "alfresco/s/api/people?alf_ticket=";

        /// <summary>
        /// GET - This api will return all the users
        /// </summary>
        public static readonly string GetUsers = AppConfigKeys.ServiceUrl + "alfresco/s/api/people?alf_ticket=";

        /// <summary>
        /// POST - This api will return user details based on username
        /// </summary>
        public static readonly string GetUser = AppConfigKeys.ServiceUrl + "alfresco/s/api/people/";
        #endregion

        #region "Administration - Groups"
        /// <summary>
        /// GET - This api will return all groups
        /// </summary>
        public static readonly string GetGroups = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups?alf_ticket=";
        #endregion

        #region Folder File
        public static readonly string SearchfileURL = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/search";
        public static readonly string Folder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/datalists/lists/site/" + AppConfigKeys.Site + "/documentLibrary?alf_ticket=";
        public static readonly string SubFolder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/treenode/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string AddFolder = AppConfigKeys.ServiceUrl + "alfresco/s/api/site/folder/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string File = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/doclist/1/site/" + AppConfigKeys.Site + "/documentLibrary/";
        #endregion
    }
}
