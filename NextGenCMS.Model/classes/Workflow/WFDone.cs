using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WFDone
    {
        public string prop_bpm_status { get; set; }
        public string assoc_packageItems_added { get; set; }
        public string assoc_packageItems_removed { get; set; }
        public string prop_bpm_comment { get; set; }
        public string prop_transitions { get; set; }
    }

    public class WFDoneModel
    {
        public string activitiid { get; set; }
        public WFDone wfDone { get; set; }
    }

}
