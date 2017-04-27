using DotCMIS.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.Workflow;
using NextGenCMS.Model.constants;
using NextGenCMS.Model.Alfresco.workflow;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace NextGenCMS.BL.classes
{
    public class WorkflowBL : IWorkflowBL
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        private ISession session = null;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;

        public WorkflowBL(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
        }

        public List<WorkFlowModel> GetAllTask()
        {
            try
            {
                string data = string.Empty;
                if (HttpContext.Current.Items[Filter.Token] != null)
                {
                    data = this._apiHelper.Get(ServiceUrl.TaskList + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
                }
                RootObject response = JsonConvert.DeserializeObject<RootObject>(data);
                return this.Map(response);
            }
            catch
            {
                throw;
            }
        }

        private List<WorkFlowModel> Map(RootObject rootObject)
        {
            List<WorkFlowModel> dataObject = new List<WorkFlowModel>();
            List<Datum> data = rootObject.data;
            foreach (Datum obj in data)
            {
                dataObject.Add(new WorkFlowModel
                {

                    Activityid = obj.workflowInstance.id,
                    dueDate = Convert.ToDateTime(obj.workflowInstance.dueDate),
                    firstName = obj.workflowInstance.initiator.firstName,
                    startDate = Convert.ToDateTime(obj.workflowInstance.startDate),
                    state = obj.properties.bpm_status,
                    outcome = obj.title,
                    title = obj.workflowInstance.description,
                    pid = obj.id,
                    status = obj.properties.bpm_status,
                    comment = obj.properties.bpm_comment,
                    OwnerUsername = obj.owner != null ? obj.owner.userName : obj.workflowInstance.initiator.userName,
                    fullName = obj.workflowInstance.initiator != null ? obj.workflowInstance.initiator.firstName + " " + obj.workflowInstance.initiator.lastName : string.Empty,
                    priority = GetPriority(obj.properties.bpm_priority),
                    taskId = obj.properties.bpm_taskId,
                    workflowid = obj.workflowInstance.id,
                    description = obj.description
                });
            }
            return dataObject.OrderBy(x => x.priority).ToList();
        }

        private string GetPriority(int? priority)
        {
            if (priority == 1)
            {
                return "High";
            }
            else if (priority == 2)
            {
                return "Medium";
            }
            else if (priority == 3)
            {
                return "Low";
            }
            return string.Empty;
        }

        public void CreateWorkflow(CreateWorkflowModel objModel)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                objModel.workModel.items = new List<string>();
                objModel.workModel.items.Add(objModel.docId);
                data = this._apiHelper.Post(ServiceUrl.CreateProcessURL + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(objModel.workModel));
                //var converter = new ExpandoObjectConverter();
                //dynamic dataObject = JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
                //var ProcessId = dataObject.entry.id;
                //ItemBodyModel item = new ItemBodyModel();
                //item.id = objModel.docId;
                //var retVal = this._apiHelper.Post(ServiceUrl.CreateProcessItems + ProcessId + "/items" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(item));
            }

        }

        public FRootObject GetWorkflowFile(string id)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.WfFile + id + "/items" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<FRootObject>(data);
        }

        public void WorkflowUpdate(WFUpdateModel updateModel)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Post(ServiceUrl.WFUpdate + updateModel.wfId + "/variables" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(updateModel.WFUpdate));
            }
        }

        public void ApproveReject(WFApproveRejectModel model)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {

                data = this._apiHelper.Post(ServiceUrl.ApproveReject + model.activitiid + "/formprocessor" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(model.WFAprroveReject));
            }
        }
        public void DoneTask(WFDoneModel model)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Post(ServiceUrl.ApproveReject + model.activitiid + "/formprocessor" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(model.wfDone));
            }
        }

        public List<AllTaskModel> GetAllTasks(string wfid)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.AllWF + wfid + "?includeTasks=true&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return this.MapAll(JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfRootObject>(data));
        }
        public List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance> GetWorkFlow(string username)
        {
            List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance> list = new List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance>();
            string data = string.Empty;
            string activeWf = string.Empty; 
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.CompletedWF +"?state=COMPLETED&initiator="+username+"&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            } 
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                activeWf = this._apiHelper.Get(ServiceUrl.CompletedWF + "?initiator=" + username + "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            NextGenCMS.Model.Alfresco.workflow.WfAllRootObject wf = JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfAllRootObject>(data);
            NextGenCMS.Model.Alfresco.workflow.WfAllRootObject wfActive = JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfAllRootObject>(activeWf);
            if (wfActive.data != null && wfActive.data.Any())
            {
                list.AddRange(wfActive.data);
            }
        public WfRootObject GetCaseDetails(string wfid)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.AllWF + wfid + "?includeTasks=true&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<WfRootObject>(data);
        }

            if (wf.data != null && wf.data.Any())
            {
                list.AddRange(wf.data);
            }            
            return list;
        }
        private List<AllTaskModel> MapAll(NextGenCMS.Model.Alfresco.workflow.WfRootObject dataObject)
        {
            List<AllTaskModel> model = new List<AllTaskModel>();
            foreach (NextGenCMS.Model.Alfresco.workflow.Task task in dataObject.data.tasks)
            {
                model.Add(new AllTaskModel
                {
                    bpm_comment = task.properties.bpm_comment,
                    cm_owner = task.properties.cm_owner,
                    cm_created = task.properties.cm_created,
                    Created = Convert.ToDateTime(task.properties.bpm_startDate),
                    status = task.properties.bpm_status
                });
            }
            List<AllTaskModel> modelList = model.OrderByDescending(item => item.Created).ToList();
            modelList.Remove(modelList.Take(1).FirstOrDefault());
            return modelList;
        }
    }
}
