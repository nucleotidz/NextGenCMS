﻿using NextGenCMS.API.Filters;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NextGenCMS.API.Controllers
{
    [Secure]
    [RoutePrefix("api/workflow")]
    public class WorkflowController : ApiController
    {
        IWorkflowBL workflowBl;
        public WorkflowController(IWorkflowBL _workflowBl)
        {
            this.workflowBl = _workflowBl;
        }

        [Route("Get")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, this.workflowBl.GetAllTask());
        }

        [HttpPost]
        [Route("Workflow")]
        public HttpResponseMessage CreateWorkflow(CreateWorkflowModel objModel)
        {
            this.workflowBl.CreateWorkflow(objModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [Route("Get/File/{Id}")]
        public HttpResponseMessage GetFile(string Id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this.workflowBl.GetWorkflowFile(Id));
        }
    }
}
