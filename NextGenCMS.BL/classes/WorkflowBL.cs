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
                    creatorUserName = obj.workflowInstance.initiator != null ? obj.workflowInstance.initiator.userName : string.Empty,
                    fullName = obj.workflowInstance.initiator != null ? obj.workflowInstance.initiator.firstName + " " + obj.workflowInstance.initiator.lastName : string.Empty,
                    priority = GetPriority(obj.properties.bpm_priority),
                    taskId = obj.properties.bpm_taskId,
                    workflowid = obj.workflowInstance.id,
                    description = obj.description,
                    cm_name = obj.properties.cm_name
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

        public void Reassign(ReassignModel objReassign)
        {
            WorkflowReassignModel objParams = new WorkflowReassignModel();
            WorkflowReassignModel OjbCommentParams = new WorkflowReassignModel();
            objReassign.comment = this.FormatCommentHistory(objReassign.oldComment, objReassign.assigneeName, objReassign.comment);
            if (!objReassign.IsResolve)
            {
                objParams.assignee = objReassign.username;
                objParams.state = "delegated";
                OjbCommentParams.state = "resolved";
                OjbCommentParams.variables = new List<ReassignVariableModel>();
                OjbCommentParams.variables.Add(new ReassignVariableModel
                {
                    name = "bpm_comment",
                    type = "d:text",
                    scope = "global",
                    value = objReassign.comment
                });
            }
            else
            {
                objParams.state = "resolved";
                objParams.variables = new List<ReassignVariableModel>();
                objParams.variables.Add(new ReassignVariableModel
                {
                    name = "bpm_comment",
                    type = "d:text",
                    scope = "global",
                    value = objReassign.comment
                });
            }
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                if (!objReassign.IsResolve)
                {
                    data = this._apiHelper.Put(ServiceUrl.WFUpdate + objReassign.taskId + "?select=state,variables&alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(OjbCommentParams));
                    data = this._apiHelper.Put(ServiceUrl.WFUpdate + objReassign.taskId + "?select=state,assignee,variables&alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(objParams));
                }
                else
                {
                    data = this._apiHelper.Put(ServiceUrl.WFUpdate + objReassign.taskId + "?select=state,variables&alf_ticket=" + HttpContext.Current.Items[Filter.Token], JsonConvert.SerializeObject(objParams));

                }
            }
        }

        private string FormatCommentHistory(string oldComment, string user, string newComment)
        {
            string retVal = string.Empty;
            if (string.IsNullOrEmpty(oldComment))
            {
                retVal = user + ';' + newComment;
            }
            else
            {
                retVal = oldComment + ',' + user + ';' + newComment;
            }
            return retVal.Length <= 250 ? retVal : retVal.Substring(0, 250);
        }

        public List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance> GetWorkFlow(string username)
        {
            List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance> list = new List<NextGenCMS.Model.Alfresco.workflow.WorkflowInstance>();
            string data = string.Empty;
            string activeWf = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.WorkflowReport + ServiceUrl.WorkflowCompleted + "&initiator=" + username + "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                activeWf = this._apiHelper.Get(ServiceUrl.WorkflowReport + "&initiator=" + username + "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            NextGenCMS.Model.Alfresco.workflow.WfAllRootObject wf = JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfAllRootObject>(data);
            NextGenCMS.Model.Alfresco.workflow.WfAllRootObject wfActive = JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfAllRootObject>(activeWf);
            if (wfActive.data != null && wfActive.data.Any())
            {
                list.AddRange(wfActive.data);
            }
            if (wf.data != null && wf.data.Any())
            {
                list.AddRange(wf.data);
            }

            return list;
        }
        public NextGenCMS.Model.Alfresco.workflow.WfRootObject GetCaseDetails(string wfid)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.AllWF + wfid + "?includeTasks=true&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<NextGenCMS.Model.Alfresco.workflow.WfRootObject>(data);
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
                    Created = Convert.ToDateTime(task.properties.cm_created),
                    status = task.properties.bpm_status,
                    outcome = task.outcome,
                    title = task.title
                });
            }
            List<AllTaskModel> modelList = model.OrderByDescending(item => item.Created).ToList();
            modelList = AddTaskHistory(modelList);
            return modelList;
        }
        private List<AllTaskModel> AddTaskHistory(List<AllTaskModel> modelList)
        {
            List<AllTaskModel> retList = modelList;
            AllTaskModel review = modelList.Where(a => a.title == "Review").FirstOrDefault();
            AllTaskModel approved = modelList.Where(a => a.title == "Approved").FirstOrDefault();
            AllTaskModel rejected = modelList.Where(a => a.title == "Rejected").FirstOrDefault();
            if (approved != null && approved.bpm_comment.IndexOf(';') > 0)
            {
                review.bpm_comment = approved.bpm_comment;
                approved.bpm_comment = string.Empty;
            }
            if (rejected != null && rejected.bpm_comment.IndexOf(';') > 0)
            {
                review.bpm_comment = rejected.bpm_comment;
                rejected.bpm_comment = string.Empty;
            }
            string comment = review.bpm_comment;
            string firstComm = string.Empty;
            string firstUser = string.Empty;
            if (comment.IndexOf(',') > 0)
            {
                var commenthistory = comment.Split(',');
                for (int i = 0; i < commenthistory.Length; i++)
                {
                    var commList = commenthistory[i].Split(';');
                    if (i > 0)
                    {
                        AllTaskModel taskModel = new AllTaskModel();
                        taskModel.title = review.title;
                        taskModel.status = review.status;
                        taskModel.outcome = review.outcome;
                        taskModel.bpm_comment = commList[1];
                        taskModel.cm_owner = commList[0];
                        taskModel.cm_created = review.cm_created;
                        taskModel.Created = review.Created;
                        taskModel.id = i;
                        modelList.Add(taskModel);
                    }
                    else
                    {
                        firstComm = commList[1];
                        firstUser = commList[0];
                    }
                }
                review.bpm_comment = firstComm;
                review.cm_owner = firstUser;
                retList = modelList;
            }
            else if (!string.IsNullOrEmpty(comment))
            {
                var comList = comment.Split(';');
                if (comList.Length > 1)
                {
                    firstComm = comList[1];
                    firstUser = comList[0];
                }
                review.bpm_comment = firstComm;
                review.cm_owner = firstUser;
                retList = modelList;
            }
            return retList;
        }
    }
}
