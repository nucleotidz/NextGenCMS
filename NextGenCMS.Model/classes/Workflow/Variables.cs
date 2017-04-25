using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class Variables
    {
        public string bpm_assignee { get; set; }
        public bool bpm_sendEMailNotifications { get; set; }
        public int bpm_workflowPriority { get; set; }
        public DateTime bpm_workflowDueDate { get; set; }
        public string bpm_workflowDescription { get; set; }
    }
}
