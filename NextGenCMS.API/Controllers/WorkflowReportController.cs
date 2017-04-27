using NextGenCMS.API.Filters;
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
    [RoutePrefix("api/report/workflow")]
    public class WorkflowReportController : ApiController
    {
        IWorkflowReport _workflowReport;

        public WorkflowReportController(IWorkflowReport workflowReport)
        {
            this._workflowReport = workflowReport;
        }

        [HttpGet]
        [Route("all/{username}")]
        public HttpResponseMessage GetAllWorkflows(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetAllWorkflows(username));
        }

        [HttpGet]
        [Route("active/{username}")]
        public HttpResponseMessage GetActiveWorkflows(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetActiveWorkflows(username));
        }

        [HttpGet]
        [Route("completed/{username}")]
        public HttpResponseMessage GetCompletedWorkflows(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetCompletedWorkflows(username));
        }
    }
}
