
namespace NextGenCMS.API.Controllers
{
    #region Namespaces
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.BL.interfaces;
    using NextGenCMS.Model.classes;
    using NextGenCMS.Model.classes.administration;
    using NextGenCMS.API.Filters;
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
        /// <param name="loginModel">loginModel</param>
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
        [Route("users/{searchText}")]
        public HttpResponseMessage SearchUsers(string searchText)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetUsers(searchText));
        }

        /// <summary>
        /// This method will search users based on search text
        /// </summary>
        /// <param name="searchText">searchText</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        [Route("users")]
        public HttpResponseMessage GetUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetUsers(string.Empty));
        }
        #endregion

        #region "Groups
        [HttpGet]
        [Route("groups")]
        public HttpResponseMessage GetGroups()
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._administration.GetGroups());
        }
        #endregion
    }
}
