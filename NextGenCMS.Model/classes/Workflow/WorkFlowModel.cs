using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WorkFlowModel
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string state { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public string Activityid { get; set; }

    }
}
