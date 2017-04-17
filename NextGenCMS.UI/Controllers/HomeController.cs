using NextGenCMS.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    [Secure]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ToolPane()
        {
            return View();
        }
        public ActionResult Adminstration()
        {
            return View();
        }
    }
}