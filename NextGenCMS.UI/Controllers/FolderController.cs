using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    public class FolderController : Controller
    {
        // GET: Folder
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult AddFolderPopup()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}