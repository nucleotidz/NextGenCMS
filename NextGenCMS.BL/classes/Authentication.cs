﻿
namespace NextGenCMS.BL.classes
{
    #region Namespaces
    using System;
    using System.Web;
    using Newtonsoft.Json;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.APIHelper.interfaces;
    using NextGenCMS.DL.interfaces;
    using NextGenCMS.Model.classes;
    using NextGenCMS.BL.interfaces;
    using NextGenCMS.Model.constants;
    using NextGenCMS.Model.classes.authentication;
    #endregion

    /// <summary>
    /// This method will handle Business logic of Authentication
    /// </summary>
    public class Authentication : IAuthentication
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// data layer object
        /// </summary>
        private readonly IAuthenticationRepository _repository;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;

        #region Constructor
        /// <summary>
        /// COnstructor to initialize objects
        /// </summary>
        /// <param name="repository">IAuthenticationRepository</param>
        /// <param name="apiHelper">IAPIHelper</param>
        public Authentication(IAuthenticationRepository repository, IAPIHelper apiHelper)
        {
            _repository = repository;
            _apiHelper = apiHelper;
        }
        #endregion

        #region "Public Methods"
        /// <summary>
        /// This method will authenticate the user
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        public string AuthenticateUser(LoginModel loginModel)
        {
            string token = _apiHelper.Post(ServiceUrl.Login, JsonConvert.SerializeObject(loginModel));
            LoginToken _loginToken = JsonConvert.DeserializeObject<LoginToken>(token);
            return _loginToken.data.ticket;
        }

        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <returns></returns>
        public string Logout(string ticket)
        {
            string data = this._apiHelper.Delete(ServiceUrl.Logout + ticket + "?alf_ticket=" + ticket);
            string response = JsonConvert.DeserializeObject<string>(data);
            return response;
        }
        #endregion

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
                    _repository.Dispose();
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
