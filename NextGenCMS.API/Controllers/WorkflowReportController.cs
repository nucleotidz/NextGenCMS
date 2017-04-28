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

        [HttpGet]
        [Route("due/today/{username}")]
        public HttpResponseMessage GetWorkflowsDueToday(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsDueToday(username));
        }

        [HttpGet]
        [Route("due/tomorrow/{username}")]
        public HttpResponseMessage GetWorkflowsDueTomorrow(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsDueTomorrow(username));
        }

        [HttpGet]
        [Route("due/next7days/{username}")]
        public HttpResponseMessage GetWorkflowsDueNext7Days(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsDueNext7Days(username));
        }

        [HttpGet]
        [Route("overdue/{username}")]
        public HttpResponseMessage GetWorkflowsOverdue(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsOverdue(username));
        }

        [HttpGet]
        [Route("noduedate/{username}")]
        public HttpResponseMessage GetWorkflowsNoDueDate(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsNoDueDate(username));
        }

        [HttpGet]
        [Route("started/last7days/{username}")]
        public HttpResponseMessage GetWorkflowsStartedinLast7days(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsStartedinLast7days(username));
        }

        [HttpGet]
        [Route("started/last14days/{username}")]
        public HttpResponseMessage GetWorkflowsStartedinLast14days(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsStartedinLast14days(username));
        }

        [HttpGet]
        [Route("started/last28days/{username}")]
        public HttpResponseMessage GetWorkflowsStartedinLast28days(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsStartedinLast28days(username));
        }

        [HttpGet]
        [Route("priority/high/{username}")]
        public HttpResponseMessage GetWorkflowsHighPriority(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsHighPriority(username));
        }

        [HttpGet]
        [Route("priority/medium/{username}")]
        public HttpResponseMessage GetWorkflowsMediumPriority(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsMediumPriority(username));
        }

        [HttpGet]
        [Route("priority/low/{username}")]
        public HttpResponseMessage GetWorkflowsLowPriority(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, this._workflowReport.GetWorkflowsLowPriority(username));
        }
    }
}
