using DotCMIS.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.Workflow;
using NextGenCMS.Model.constants;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
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
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.TaskList + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return this.Map(JsonConvert.DeserializeObject<RootObject>(data));
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
                    OwnerUsername = obj.owner.userName,
                    fullName = obj.owner.firstName + " " + obj.owner.lastName,
                    priority = GetPriority(obj.properties.bpm_priority),
                    taskId = obj.properties.bpm_taskId,
                    workflowid = obj.workflowInstance.id,
                    description = obj.description
                });
            }
            return dataObject.OrderBy(x => x.priority).ToList();
        }

        private string GetPriority(int priority)
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
                data = this._apiHelper.Post(ServiceUrl.CreateProcessURL + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(objModel.workModel));
                var converter = new ExpandoObjectConverter();
                dynamic dataObject = JsonConvert.DeserializeObject<ExpandoObject>(data, converter);
                var ProcessId = dataObject.entry.id;
                ItemBodyModel item = new ItemBodyModel();
                item.id = objModel.docId;
                var retVal = this._apiHelper.Post(ServiceUrl.CreateProcessItems + ProcessId + "/items" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(item));
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
               
                data = this._apiHelper.Post(ServiceUrl.ApproveReject+"activiti$2988/formprocessor" + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(model.WFAprroveReject));
            }
        }
    }
}
