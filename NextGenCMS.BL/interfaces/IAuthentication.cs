﻿
namespace NextGenCMS.BL.interfaces
{
    #region Namespaces
    using System;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.Model.classes;
    #endregion

    /// <summary>
    /// This interface will handle Business logic of Authentication
    /// </summary>
    public interface IAuthentication : IDisposable
    {
        /// <summary>
        /// This method will authenticate the user
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        string AuthenticateUser(LoginModel loginModel);
    }
}