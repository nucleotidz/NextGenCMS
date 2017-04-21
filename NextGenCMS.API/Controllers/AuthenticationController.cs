
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
    #endregion

    /// <summary>
    /// Description : This file contians Web api methods for Authentication
    /// </summary>
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
        #region "Private Fields"
        /// <summary>
        /// IAuthentication business layer interface object
        /// </summary>
        private IAuthentication _authentication;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constroctor for AuthenticationController
        /// </summary>
        /// <param name="authentication">IAuthentication</param>
        public AuthenticationController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method will authenticate user login
        /// </summary>
        /// <param name="loginModel">loginModel</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage AuthenticateUser(LoginModel loginModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._authentication.AuthenticateUser(loginModel));
        }
        /// <summary>
        /// This method will delete the ticket and logout the user
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        [HttpDelete]
        [Route("logout/{ticket}")]
        public HttpResponseMessage Logout(string ticket)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._authentication.Logout(ticket));
        }
        #endregion
    }
}
