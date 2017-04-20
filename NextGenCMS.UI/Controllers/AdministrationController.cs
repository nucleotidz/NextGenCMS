using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AdministrationController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            return View();
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManagement()
        {
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GroupManagement()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserPopup()
        {
            return View();
        }
    }
}