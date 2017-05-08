using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class AllTaskModel
    {
        public int id { get; set; }
        public string cm_owner { get; set; }
        public string bpm_comment { get; set; }
        public string cm_created { get; set; }
        public DateTime Created { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string outcome { get; set; }
    }
}
