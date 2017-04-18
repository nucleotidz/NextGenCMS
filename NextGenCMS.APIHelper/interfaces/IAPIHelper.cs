namespace NextGenCMS.APIHelper.interfaces
{
    #region Namespaces
    using System;
    using System.Web;
    #endregion

    public interface IAPIHelper
    {
        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>response</returns>
        string Get(string uri);

        /// <summary>
        /// Used for posting to API
        /// </summary>
        /// <param name="uri">Address of api</param>
        /// <param name="parameters">json object</param>
        /// <returns>response</returns>
        string Post(string uri, string parameters);

        /// <summary>
        /// Used for posting to API
        /// </summary>
        /// <param name="request">HttpRequest request object</param>
        /// <param name="uri">Address of URI</param>
        /// <returns>response</returns>
        string Post(HttpRequest request, string uri);
    }
}
