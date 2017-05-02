
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
        #region "Private fields"
        /// <summary>
        /// disposed is used to reallocate memory of Unused Objects
        /// </summary>
        private bool _disposed;
        #endregion

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
                            return response.message;
                        }
                    }
                }
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
                    string response = sr.ReadToEnd().Trim();
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
                        }
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// Used for posting to API
        /// </summary>
        /// <param name="uri">Address of api</param>
        /// <param name="parameters">json object</param>
        /// <returns>response</returns>
        public string Put(string uri, string parameters)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                WebRequest req = WebRequest.Create(uri);
                req.ContentType = ApiHelper.application_json;
                req.Timeout = 1000000;
                req.Method = ApiHelper.Put;
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
                        }
                    }
                }
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

        public void DownLoad(string url, string fileName)
        {
         
            MemoryStream fileContent = null;
            Uri uri = new Uri(url);
            WebRequest webRequest = WebRequest.Create(uri);
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                byte[] response = new System.Net.WebClient().DownloadData(url);   
                fileContent = new MemoryStream();
                Stream responseStream = webResponse.GetResponseStream();                            
                HttpContext.Current.Response.ContentType = webResponse.Headers["content-type"];
                string header = string.Format("attachment;filename=" + fileName);
                HttpContext.Current.Response.AddHeader("Content-Disposition", header);
                HttpContext.Current.Response.OutputStream.Write(response, 0, response.Length);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.End();
            }

        }

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
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
