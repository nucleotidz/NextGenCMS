using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.Folder
{
    public class RootObject
    {
        public string container { get; set; }
        public Permissions permissions { get; set; }
        public List<Datalist> datalists { get; set; }
    }
}
