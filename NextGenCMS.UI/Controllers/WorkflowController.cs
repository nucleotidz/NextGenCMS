﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextGenCMS.UI.Controllers
{
    public class WorkflowController : Controller
    {
        // GET: Workflow
        public ActionResult WorkflowDetail()
        {
            return View();
        }
        public ActionResult TaskList()
        {
            return View();
        }
        public  ActionResult CreateWorkflow()
        {
            return View();
        }
        public ActionResult ViewEditWorkFlow()
        {
            return View();
        }

        public ActionResult MyWorkFlow()
        {

            return View();
        }

        public ActionResult ProcessDiagram()
        {

            return View();
        }
    }
}