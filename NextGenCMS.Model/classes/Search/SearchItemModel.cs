using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Search
{
    public class SearchItemModel
    {       
        public string type { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string modifiedOn { get; set; }
        public string modifiedByUser { get; set; }
        public string modifiedBy { get; set; }
        public string fromDate { get; set; }
        public int size { get; set; }
        public string mimetype { get; set; }
    }
}
