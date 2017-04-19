using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.Folder
{
    public class SRootObject
    {
        public int totalResults { get; set; }
        public bool resultsTrimmed { get; set; }
        public Parent parent { get; set; }
        public List<Item> items { get; set; }
    }
}
