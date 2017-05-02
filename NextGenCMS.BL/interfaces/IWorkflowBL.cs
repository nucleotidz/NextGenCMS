using NextGenCMS.Model.Alfresco.workflow;
using NextGenCMS.Model.classes.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.BL.interfaces
{
    public interface IWorkflowBL
    {
        List<WorkFlowModel> GetAllTask();

        void CreateWorkflow(CreateWorkflowModel objModel);
        FRootObject GetWorkflowFile(string id);
        void WorkflowUpdate(WFUpdateModel updateModel);
        void ApproveReject(WFApproveRejectModel model);
        void DoneTask(WFDoneModel model);
        List<AllTaskModel> GetAllTasks(string wfid);
        NextGenCMS.Model.Alfresco.workflow.WfRootObject GetCaseDetails(string wfid);
        List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance> GetWorkFlow(string username);
        void Reassign(int taskId, string username, bool IsResolve);
        
    }
}
