using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Home()
        {
            return View();
        }
        // GET: Administration
        public ActionResult UserManagement()
        {
            return View();
        }
        // GET: Administration
        public ActionResult GroupManagement()
        {
            return View();
        }

        public ActionResult AddUserPopup()
        {
            return View();
        }
    }
}