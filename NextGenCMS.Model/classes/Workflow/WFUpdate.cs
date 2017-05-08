using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WFUpdate
    {
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string scope { get; set; }
    }

    public class WFUpdateModel
    {
        public string wfId { get; set; }
        public List<WFUpdate> WFUpdate { get; set; }
    }

    public class WorkflowUpdateWrapper
    {
        public WFUpdateModel workflowModel { get; set; }
        public string oldComment { get; set; }
        public string assignee { get; set; }
    }
}
