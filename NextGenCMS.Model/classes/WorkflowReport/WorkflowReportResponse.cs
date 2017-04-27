using System.Collections.Generic;

namespace NextGenCMS.Model.classes.WorkflowReport
{
    public class WorkflowReportResponse
    {
        public List<WorkflowInstance> data { get; set; }
        public Paging paging { get; set; }
    }
    public class Initiator
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
        public object endDate { get; set; }
        public string dueDate { get; set; }
        public object context { get; set; }
        public string package { get; set; }
        public Initiator initiator { get; set; }
        public string definitionUrl { get; set; }
    }

    public class Paging
    {
        public int maxItems { get; set; }
        public int skipCount { get; set; }
        public int totalItems { get; set; }
        public object totalItemsRangeEnd { get; set; }
        public string confidence { get; set; }
    }
}
