using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.Folder
{
    public class Item
    {
        public string nodeRef { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool hasChildren { get; set; }
        public UserAccess2 userAccess { get; set; }
        public List<string> aspects { get; set; }
    }
}
