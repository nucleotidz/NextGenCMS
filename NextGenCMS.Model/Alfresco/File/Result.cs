using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.File
{
    public class Result
    {
        public string action { get; set; }
        public string id { get; set; }
        public string nodeRef { get; set; }
        public bool success { get; set; }
    }
}
