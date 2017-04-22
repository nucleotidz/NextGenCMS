using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.Model.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NextGenCMS.APIHelper.classes
{
    public class Uploader : IUploader
    {      
        public void Upload(string url)
        {
            try
            {
                HttpWebRequest requestToServerEndpoint = (HttpWebRequest)WebRequest.Create(url + HttpContext.Current.Request.Form["token"]);
                string boundaryString = "----WebKitFormBoundaryYHCnoErwHmT3HVf4";
                requestToServerEndpoint.Method = WebRequestMethods.Http.Post;
                requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
                requestToServerEndpoint.KeepAlive = true;
                requestToServerEndpoint.Credentials = System.Net.CredentialCache.DefaultCredentials;

                MemoryStream postDataStream = new MemoryStream();
                StreamWriter postDataWriter = new StreamWriter(postDataStream);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data;" + "name=\"{0}\";" + "filename=\"{1}\"" + "\r\nContent-Type: {2}\r\n\r\n",
                                        "filedata", HttpContext.Current.Request.Files[0].FileName, HttpContext.Current.Request.Files[0].ContentType);
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "siteId", "ahmar");
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "containerId", "documentLibrary");
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", "uploaddirectory", "/CSC/");
                postDataWriter.Flush();

                Stream fileStream = HttpContext.Current.Request.Files[0].InputStream;
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();

                postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                postDataWriter.Flush();
                requestToServerEndpoint.ContentLength = postDataStream.Length;
                using (Stream requestStream = requestToServerEndpoint.GetRequestStream())
                {
                    postDataStream.WriteTo(requestStream);
                }
                postDataStream.Close();
                WebResponse response = requestToServerEndpoint.GetResponse();
                StreamReader responseReader = new StreamReader(response.GetResponseStream());
                string replyFromServer = responseReader.ReadToEnd();
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
    }
}
