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

        [HttpPost]
        [Route("Update/Workflow/Activity")]
        public HttpResponseMessage UpdateWf(WorkflowUpdateWrapper objWrapper)
        {
            workflowBl.WorkflowUpdate(objWrapper.workflowModel, objWrapper.oldComment, objWrapper.assignee);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        [Route("Action")]
        public HttpResponseMessage ApproveReject(WFApproveRejectModel model)
        {
            workflowBl.ApproveReject(model);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Done")]
        public HttpResponseMessage DoneTask(WFDoneModel model)
        {
            workflowBl.DoneTask(model);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("All/Task/{wfid}")]
        public HttpResponseMessage GetAllTasks(string wfid)
        {
            return Request.CreateResponse(HttpStatusCode.OK, workflowBl.GetAllTasks(wfid));
        }

        [HttpGet]
        [Route("WorkflowDetails/{wfId}")]
        public HttpResponseMessage GetWorkflowDetails(string wfId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, workflowBl.GetCaseDetails(wfId));
        }
        [HttpGet]
        [Route("Get/WF/{username}")]
        public HttpResponseMessage GetWf(string username)
        {
            return Request.CreateResponse(HttpStatusCode.OK, workflowBl.GetWorkFlow(username));
        }

        [HttpPost]
        [Route("Reassign")]
        public HttpResponseMessage Reassign(ReassignModel objReassign)
        {
            workflowBl.Reassign(objReassign);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
