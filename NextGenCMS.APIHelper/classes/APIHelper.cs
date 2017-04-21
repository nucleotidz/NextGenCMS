
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
    using System;
    #endregion

    public class APIHelper : IAPIHelper
    {

        public string Delete(string uri)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                WebRequest req = WebRequest.Create(uri);
                req.Method = ApiHelper.Delete;
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
        public void DownLoad(string url)
        {
            MemoryStream fileContent = null;
            string fileName = String.Empty;

            Uri uri = new Uri(url);
            WebRequest webRequest = WebRequest.Create(uri);
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                fileName = webResponse.Headers["Content-Disposition"].Replace("attachment; filename=", String.Empty).Replace("\"", String.Empty);
                fileContent = new MemoryStream();
                Stream responseStream = webResponse.GetResponseStream();
                byte[] responseBuffer = new byte[16 * 1024];
                int responseBytesRead;

                while (responseStream.Read(responseBuffer, 0, responseBuffer.Length) > 0)
                {
                    responseBytesRead = responseStream.Read(responseBuffer, 0, responseBuffer.Length);
                    fileContent.Write(responseBuffer, 0, responseBytesRead);
                }
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;");
                HttpContext.Current.Response.OutputStream.Write(responseBuffer, 0, responseBuffer.Length);
            }
        }

    }
}
