﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NextGenCMS.APIHelper.classes;
using NextGenCMS.UI.Model;
using System.Configuration;
using Newtonsoft.Json;

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
            NextGenCMS.APIHelper.classes.APIHelper apiCaller = new NextGenCMS.APIHelper.classes.APIHelper();
            LoginModel _loginModel = new LoginModel
            {
                password = password,
                username = username 
            };
            string token = apiCaller.Post(ConfigurationManager.AppSettings["API:URL"] + "authentication/login", JsonConvert.SerializeObject(_loginModel));
            Session["SessionContext"] = JsonConvert.DeserializeObject(token);
            return new RedirectResult(BaseURL);
        }
        // GET: Security
        public ActionResult Logout()
        {
            NextGenCMS.APIHelper.classes.APIHelper apiCaller = new NextGenCMS.APIHelper.classes.APIHelper();
            apiCaller.Delete(ConfigurationManager.AppSettings["API:URL"] + "authentication/logout/" + Session["SessionContext"].ToString());
            Session.Abandon();
            return new RedirectResult(BaseURL);
        }
    }
}