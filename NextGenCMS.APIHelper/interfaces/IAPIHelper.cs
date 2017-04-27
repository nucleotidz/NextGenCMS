namespace NextGenCMS.APIHelper.interfaces
{
    #region Namespaces
    using NextGenCMS.Model.classes;
    using System;
    using System.IO;
    using System.Web;
    #endregion

    public interface IAPIHelper : IDisposable
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
        /// <param name="uri">Address of api</param>
        /// <param name="parameters">json object</param>
        /// <returns>response</returns>
        WebResponseModel Submit(string uri, string parameters);

        void DownLoad(string uri, string fileName);

        string Delete(string uri);
    }
}
