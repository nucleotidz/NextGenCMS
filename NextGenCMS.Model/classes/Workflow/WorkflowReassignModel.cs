using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WorkflowReassignModel
    {
        public string state { get; set; }
        public string assignee { get; set; }
        public List<ReassignVariableModel> variables { get; set; }
    }
}

