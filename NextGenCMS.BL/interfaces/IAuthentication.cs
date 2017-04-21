
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

        /// <summary>
        /// This method delete the ticket and logout the user
        /// </summary>
        /// <returns></returns>
        string Logout(string ticket);
    }
}
