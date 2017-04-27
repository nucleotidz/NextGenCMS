using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.workflow
{
    public class Initiator
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class TaskDefinition
    {
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Definition
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public string startTaskDefinitionUrl { get; set; }
        public string startTaskDefinitionType { get; set; }
        public List<TaskDefinition> taskDefinitions { get; set; }
    }

    public class Owner
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Properties
    {
        public string bpm_startDate { get; set; }
        public string bpm_packageActionGroup { get; set; }
        public bool bpm_reassignable { get; set; }
        public string bpm_dueDate { get; set; }
        public int bpm_priority { get; set; }
        public string bpm_completionDate { get; set; }
        public int bpm_percentComplete { get; set; }
        public List<object> bpm_pooledActors { get; set; }
        public List<object> bpm_hiddenTransitions { get; set; }
        public string cm_owner { get; set; }
        public object bpm_context { get; set; }
        public string wf_reviewOutcome { get; set; }
        public string bpm_outcome { get; set; }
        public string bpm_package { get; set; }
        public object bpm_completedItems { get; set; }
        public string bpm_packageItemActionGroup { get; set; }
        public string bpm_description { get; set; }
        public string bpm_outcomePropertyName { get; set; }
        public object cm_content { get; set; }
        public string cm_name { get; set; }
        public string bpm_status { get; set; }
        public string bpm_comment { get; set; }
        public string cm_created { get; set; }
        public string bpm_taskId { get; set; }
        public string bpm_assignee { get; set; }
        public string initiator { get; set; }
        public bool? bpm_sendEMailNotifications { get; set; }
        public int? bpm_workflowPriority { get; set; }
        public string workflowinstanceid { get; set; }
        public string bpm_workflowDueDate { get; set; }
        public string bpm_workflowDescription { get; set; }
        public string companyhome { get; set; }
        public string initiatorhome { get; set; }
        public bool? cancelled { get; set; }
    }

    public class PropertyLabels
    {
        public string bpm_status { get; set; }
        public string wf_reviewOutcome { get; set; }
        public string bpm_priority { get; set; }
        public string bpm_workflowPriority { get; set; }
    }

    public class Initiator2
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class WorkflowInstance
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public string startDate { get; set; }
        public int priority { get; set; }
        public string message { get; set; }
        public string endDate { get; set; }
        public string dueDate { get; set; }
        public object context { get; set; }
        public string package { get; set; }
        public Initiator2 initiator { get; set; }
        public string definitionUrl { get; set; }
    }

    public class Task
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string state { get; set; }
        public string path { get; set; }
        public bool isPooled { get; set; }
        public bool isEditable { get; set; }
        public bool isReassignable { get; set; }
        public bool isClaimable { get; set; }
        public bool isReleasable { get; set; }
        public string outcome { get; set; }
        public Owner owner { get; set; }
        public object creator { get; set; }
        public Properties properties { get; set; }
        public PropertyLabels propertyLabels { get; set; }
        public WorkflowInstance workflowInstance { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
        public string startDate { get; set; }
        public int priority { get; set; }
        public string message { get; set; }
        public string endDate { get; set; }
        public string dueDate { get; set; }
        public object context { get; set; }
        public string package { get; set; }
        public Initiator initiator { get; set; }
        public string definitionUrl { get; set; }
        public object diagramUrl { get; set; }
        public string startTaskInstanceId { get; set; }
        public Definition definition { get; set; }
        public List<Task> tasks { get; set; }
    }

    public class WfRootObject
    {
        public Data data { get; set; }
    }

    public class WfAllRootObject
    {
        public List<WorkflowInstance> data { get; set; }
    }
}
