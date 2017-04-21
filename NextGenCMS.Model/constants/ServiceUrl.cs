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
        #endregion

        #region "Administration - Groups"
        /// <summary>
        /// GET - This api will return all groups
        /// </summary>
        public static readonly string GetGroups = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups?alf_ticket=";
        #endregion

        #region Folder File
        public static readonly string SearchfileURL = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/search?filters=&encodedFilters=";
        public static readonly string searchQuerystring = "&tag=&startIndex=0&sort=&site=" + AppConfigKeys.Site + "&rootNode=alfresco://company/home&repo=true&query=&pageSize=25&maxResults=0&noCache=1492749618651&spellcheck=true&highlightPrefix=&highlightPostfix=&highlightFields=cm:name,cm:description,cm:title,content,ia:descriptionEvent,ia:whatEvent,lnk:title&highlightFragmentSize=100&highlightSnippetCount=255&highlightMergeContiguous=false&highlightUsePhraseHighlighter=true";
        public static readonly string Folder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/datalists/lists/site/" + AppConfigKeys.Site + "/documentLibrary?alf_ticket=";
        public static readonly string SubFolder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/treenode/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string AddFolder = AppConfigKeys.ServiceUrl + "alfresco/s/api/site/folder/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string File = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/doclist/1/site/ahmar/documentLibrary/";
        public static readonly string FileDownload = AppConfigKeys.ServiceUrl + "alfresco/s/";
        #endregion
    }
}
