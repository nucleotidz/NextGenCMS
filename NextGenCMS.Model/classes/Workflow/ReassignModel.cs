using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class ReassignModel
    {
        public int taskId { get; set; }
        public string username { get; set; }
        public bool IsResolve { get; set; }
        public string comment { get; set; }
        public string oldComment { get; set; }
        public string assigneeName { get; set; }
    }
}
