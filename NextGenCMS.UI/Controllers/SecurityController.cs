using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    public class SecurityController : Controller
    {
        /// <summary>
        /// Gets the Base Url
        /// </summary>
        /// <value>string</value>
        public string BaseURL
        {
            get
            {
                var baseUrl = HttpContext.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
                return baseUrl.EndsWith("/", StringComparison.OrdinalIgnoreCase) ? baseUrl : baseUrl + "/";
            }
        }

        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(string username, string password)
        {
            Session["SessionContext"] = Guid.NewGuid().ToString();
            return new RedirectResult(BaseURL);
        }
    }
}