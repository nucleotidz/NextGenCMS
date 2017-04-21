
namespace NextGenCMS.BL.interfaces
{
    #region Namespaces
    using System;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.Model.classes;
    using NextGenCMS.Model.classes.authentication;
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
        /// <returns>LoginResponse</returns>
        LoginResponse AuthenticateUser(LoginModel loginModel);

        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <returns>string</returns>
        string Logout(string ticket);
    }
}
