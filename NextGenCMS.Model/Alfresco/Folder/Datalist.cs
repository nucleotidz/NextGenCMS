using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.Folder
{
    public class Datalist
    {
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string nodeRef { get; set; }
        public string itemType { get; set; }
        public Permissions2 permissions { get; set; }
    }
}
