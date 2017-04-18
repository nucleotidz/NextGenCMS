
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
                req.ContentType = CommonConstants.text_json;
                req.Timeout = 1000000;
                req.Method = CommonConstants.Post;
                //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(parameters);
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
        /// <param name="request">HttpRequest request object</param>
        /// <param name="uri">Address of URI</param>
        /// <returns>response</returns>
        public string Post(HttpRequest request, string uri)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(uri);
                newRequest.ContentType = request.ContentType;
                newRequest.Method = request.HttpMethod;
                //newRequest.Headers.Add(CommonConstants.x_thirdparty_Id, CommonConstants.ProviderSelfService);

                byte[] originalStream;
                using (var memoryStream = new MemoryStream())
                {
                    request.InputStream.CopyTo(memoryStream);
                    originalStream = memoryStream.ToArray();
                }

                using (var streamWriter = newRequest.GetRequestStream())
                {
                    if (originalStream != null && originalStream.Length > 0)
                    {
                        streamWriter.Write(originalStream, 0, originalStream.Length);
                    }
                    streamWriter.Flush();
                }

                using (WebResponse resp = newRequest.GetResponse())
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
    }
}
