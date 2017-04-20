
namespace NextGenCMS.APIHelper.classes
{
    #region Namespaces
    using System.Web;
    using System.Net.Security;
    using System.IO;
    using System.Net;
    #endregion

    #region "NextGenCMS Namespaces"
    using NextGenCMS.Model.constants;
    using NextGenCMS.APIHelper.interfaces;
    using NextGenCMS.Model.classes;
    #endregion

    public class APIHelper : IAPIHelper
    {
        /// <summary>
        /// Gets the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>response</returns>
        public string Get(string uri)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                WebRequest req = WebRequest.Create(uri);
                req.Timeout = 1000000;
                //req.Headers.Add(CommonConstants.x_thirdparty_Id, CommonConstants.ProviderSelfService);
                using (WebResponse resp = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    return response;// JsonConvert.DeserializeObject<T>(response);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for posting to API
        /// </summary>
        /// <param name="uri">Address of api</param>
        /// <param name="parameters">json object</param>
        /// <returns>response</returns>
        public string Post(string uri, string parameters)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                WebRequest req = WebRequest.Create(uri);
                //req.Headers.Add(CommonConstants.x_thirdparty_Id, CommonConstants.ProviderSelfService);
                //Add these, as we're doing a POST
                req.ContentType = ApiHelper.text_json;
                req.Timeout = 1000000;
                req.Method = ApiHelper.Post;
                if (!string.IsNullOrEmpty(parameters))
                {
                    using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                    {
                        streamWriter.Write(parameters);
                    }
                }
                using (WebResponse resp = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    return response;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for posting to API
        /// </summary>
        /// <param name="uri">Address of api</param>
        /// <param name="parameters">json object</param>
        /// <returns>response</returns>
        public WebResponseModel Submit(string uri, string parameters)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                WebRequest req = WebRequest.Create(uri);
                //req.Headers.Add(CommonConstants.x_thirdparty_Id, CommonConstants.ProviderSelfService);
                //Add these, as we're doing a POST
                req.ContentType = ApiHelper.application_json;
                req.Timeout = 1000000;
                req.Method = ApiHelper.Post;
                if (!string.IsNullOrEmpty(parameters))
                {
                    using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                    {
                        streamWriter.Write(parameters);
                    }
                }
                using (WebResponse resp = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(resp.GetResponseStream());
                    var response = new WebResponseModel
                    {
                        status = NextGenCMS.Model.constants.ApiHelper.StatusCode.Success,
                        message = sr.ReadToEnd().Trim()
                    };
                    return response;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            var response = new WebResponseModel
                            {
                                status = NextGenCMS.Model.constants.ApiHelper.StatusCode.Exception,
                                message = reader.ReadToEnd().Trim()
                            };
                            return response;
                        }
                    }
                }
                throw;
            }
        }
    }
}
