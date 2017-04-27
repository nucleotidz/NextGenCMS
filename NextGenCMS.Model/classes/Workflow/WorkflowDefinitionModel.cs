using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WorkflowDefinitionModel
    {
        public string processDefinitionKey { get; set; }
        public Variables variables { get; set; }
        public List<string> items { get; set; }
    }
}
