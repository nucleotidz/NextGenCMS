
namespace NextGenCMS.API.Controllers
{
    #region Namespaces
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Collections.Generic;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.BL.interfaces;
    using NextGenCMS.API.Filters;
    using NextGenCMS.Model.classes.administration.CreateUser;
    using NextGenCMS.Model.classes.permissions;
    using NextGenCMS.Model.classes.administration;
    #endregion

    /// <summary>
    /// Description : This file contians Web api methods for Authentication
    /// </summary>
    [RoutePrefix("api/administration")]
    [Secure]
    public class AdministrationController : ApiController
    {
        #region "Private Fields"
        /// <summary>
        /// IAdministration business layer interface object
        /// </summary>
        private IAdministration _administration;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor for AdministrationController
        /// </summary>
        /// <param name="administration">IAdministration</param>
        public AdministrationController(IAdministration administration)
        {
            this._administration = administration;
        }
        #endregion

        #region "Users"
        /// <summary>
        /// This method will authenticate user login
        /// </summary>
        /// <param name="createUser">createUser</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("user/create")]
        public HttpResponseMessage CreateUser(CreateUserRequest createUser)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.CreateUser(createUser));
        }

        /// <summary>
        /// This method will search users based on search text
        /// </summary>
        /// <param name="searchText">searchText</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        [Route("users/{searchText}/{username}")]
        public HttpResponseMessage SearchUsers(string searchText, string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetUsers(searchText, username));
        }

        /// <summary>
        /// This method will search users based on search text
        /// </summary>
        /// <param name="searchText">searchText</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        [Route("users/{username}")]
        public HttpResponseMessage GetUsers(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetUsers(string.Empty, username));
        }

        /// <summary>
        /// This method will fetch user details based on username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        [Route("user/{username}")]
        public HttpResponseMessage GetUser(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetUser(username));
        }

        /// <summary>
        /// This method will delete the users
        /// </summary>
        /// <param name="loginModel">loginModel</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("user/delete")]
        public HttpResponseMessage DeleteUser(List<string> users)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.DeleteUser(users));
        }
        #endregion

        #region "Groups"
        /// <summary>
        /// This method will create the group
        /// </summary>
        /// <param name="group">group</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("group/create")]
        public HttpResponseMessage CreateGroup(Group group)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.CreateGroup(group));
        }

        [HttpGet]
        [Route("groups")]
        public HttpResponseMessage GetGroups()
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetGroups());
        }

        [HttpGet]
        [Route("groups/search/{searchText}")]
        public HttpResponseMessage SearchGroups(string searchText)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.SearchGroups(searchText));
        }

        /// <summary>
        /// This method will delete the groups
        /// </summary>
        /// <param name="groups">groups</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("group/delete")]
        public HttpResponseMessage DeleteGroup(List<string> groups)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.DeleteGroup(groups));
        }
        #endregion

        #region "Permissions"
        [HttpGet]
        [Route("permissions/{nodeId}")]
        public HttpResponseMessage GetPermissions(string nodeId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetPermissions(nodeId));
        }

        [HttpGet]
        [Route("search/userandgroups/{searchText}")]
        public HttpResponseMessage SearchUserAndGroups(string searchText)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.SearchUserAndGroups(searchText));
        }

        /// <summary>
        /// This method will save the folder or file permissions
        /// </summary>
        /// <param name="createUser">createUser</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("permissions/save")]
        public HttpResponseMessage SavePermissions(SavePermission permissions)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.SavePermissions(permissions));
        }
        #endregion
    }
}
