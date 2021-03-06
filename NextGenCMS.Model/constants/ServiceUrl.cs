﻿using System;
using System.IO;
using System.Web;

namespace NextGenCMS.Model.constants
{
    public static class ServiceUrl
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

        /// <summary>
        /// Token
        /// </summary>
        public static readonly string AlfTicket = "&alf_ticket=";

        /// <summary>
        /// Token
        /// </summary>
        public static readonly string Alf_Ticket = "/alf_ticket=";
        /// <summary>
        /// Token
        /// </summary>
        public static readonly string Alf_Ticket_QM = "?alf_ticket=";
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

        /// <summary>
        /// DELETE - This api will delete user based on username
        /// </summary>
        public static readonly string DeleteUser = AppConfigKeys.ServiceUrl + "alfresco/s/api/people/";

        public static readonly string AddUserRole = AppConfigKeys.ServiceUrl + "/alfresco/s/api/sites/" + AppConfigKeys.Site + "/invitations?alf_ticket=";

        public static readonly string GetUserSites = AppConfigKeys.ServiceUrl + "alfresco/s/api/people/";

        public static readonly string GetSiteUsers = AppConfigKeys.ServiceUrl + "alfresco/s/api/sites/" + AppConfigKeys.Site + "/memberships?size=250&authorityType=USER&nf=";

        #endregion

        #region "Administration - Groups"
        /// <summary>
        /// GET - This api will return all groups
        /// </summary>
        public static readonly string GetGroups = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups?alf_ticket=";
        /// <summary>
        /// GET - This api will return all groups based on search text
        /// </summary>
        public static readonly string SearchGroups = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups?maxItems=250&skipCount=0&sortBy=displayName&shortNameFilter=";

        public static readonly string AddGroups = AppConfigKeys.ServiceUrl + "alfresco/s/api/rootgroups/";

        public static readonly string GroupExistSuffix = "/parents?level=ALL&maxSize=10";
        public static readonly string Groups = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups/";

        public static readonly string GroupUser = AppConfigKeys.ServiceUrl + "alfresco/s/api/groups/";

        public static readonly string GroupUserSuffix = "/children";

        public static readonly string GetGroupUserSuffix = "/children?sortBy=displayName&maxItems=50&skipCount=0";
        #endregion

        #region Folder File
        public static readonly string SearchfileURL = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/search?filters=&encodedFilters=";
        public static readonly string searchQuerystring = "&tag=&startIndex=0&sort=&site=" + AppConfigKeys.Site + "&rootNode=alfresco://company/home&repo=false&pageSize=25&maxResults=0&noCache=1492749618651&spellcheck=true&highlightPrefix=&highlightPostfix=&highlightFields=cm:name,cm:description,cm:title,content,ia:descriptionEvent,ia:whatEvent,lnk:title&highlightFragmentSize=100&highlightSnippetCount=255&highlightMergeContiguous=false&highlightUsePhraseHighlighter=true";
        public static readonly string Folder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/datalists/lists/site/" + AppConfigKeys.Site + "/documentLibrary?alf_ticket=";
        public static readonly string SubFolder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/treenode/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string AddFolder = AppConfigKeys.ServiceUrl + "alfresco/s/api/site/folder/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string File = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/doclist/1/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string FileDownload = AppConfigKeys.ServiceUrl + "alfresco/s/";
        public static readonly string Checkout = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/action/checkout/site/";
        public static readonly string CMISApi = AppConfigKeys.ServiceUrl + "/alfresco/api/-default-/public/cmis/versions/1.0/atom/";
        public static readonly string DeleteFile = AppConfigKeys.ServiceUrl + "/alfresco/s/slingshot/doclib/action/file/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string DeleteFolder = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/action/folder/site/" + AppConfigKeys.Site + "/documentLibrary/";
        public static readonly string GetVersion = AppConfigKeys.ServiceUrl + "alfresco/s/api/version?nodeRef=";

        #endregion

        #region WorkflowAPI
        public static readonly string CreateProcessURL = AppConfigKeys.ServiceUrl + "/alfresco/api/-default-/public/workflow/versions/1/processes";
        public static readonly string CreateProcessItems = AppConfigKeys.ServiceUrl + "alfresco/api/-default-/public/workflow/versions/1/processes/";
        public static readonly string TaskList = AppConfigKeys.ServiceUrl + "alfresco/s/api/task-instances";
        public static readonly string WfFile = AppConfigKeys.ServiceUrl + "alfresco/api/-default-/public/workflow/versions/1/processes/";
        public static readonly string WFUpdate = AppConfigKeys.ServiceUrl + "alfresco/api/-default-/public/workflow/versions/1/tasks/";
        public static readonly string ApproveReject = AppConfigKeys.ServiceUrl + "alfresco/service/api/task/";
        public static readonly string AllWF = AppConfigKeys.ServiceUrl + "alfresco/s/api/workflow-instances/";
        public static readonly string CompletedWF = AppConfigKeys.ServiceUrl + "alfresco/s/api/workflow-instances";
        #endregion

        #region "Workflow Report"
        public static readonly string WorkflowReport = AppConfigKeys.ServiceUrl + "/alfresco/s/api/workflow-instances?exclude=jbpm$wcmwf:*,jbpm$wf:articleapproval,activiti$publishWebContent,jbpm$publishWebContent,jbpm$inwf:invitation-nominated,jbpm$imwf:invitation-moderated,activiti$activitiInvitationModerated,activiti$activitiInvitationNominated,activiti$activitiInvitationNominatedAddDirect&skipCount=0&maxItems=50";
        public static readonly string WorkflowCompleted = "&pooledTasks=false&state=COMPLETED";
        public static readonly string WorkflowInitiator = "&initiator=";
        public static readonly string WorkflowDueAfter = "&dueAfter=";
        public static readonly string WorkflowDueBefore = "&dueBefore=";
        public static readonly string WorkflowStartedAfter = "&startedAfter=";
        public static readonly string WorkflowHighPriority = "&priority=1";
        public static readonly string WorkflowMediumPriority = "&priority=2";
        public static readonly string WorkflowLowPriority = "&priority=3";
        #endregion

        #region "Manage Permissions"
        public static readonly string SearchUserAndGroups = AppConfigKeys.ServiceUrl + "share/service/components/people-finder/authority-query?authorityType=all&maxResults=100&defGroupsFor=ahmar&filter=";
        public static readonly string Permissions = AppConfigKeys.ServiceUrl + "alfresco/s/slingshot/doclib/permissions/workspace/SpacesStore/";
        #endregion
    }
}
