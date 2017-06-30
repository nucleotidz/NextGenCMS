using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.permissions
{
    public class SavePermission
    {
        public bool isInherited { get; set; }
        public List<PostPermission> permissions { get; set; }
        public string nodeId { get; set; }
    }
    public class PostPermission
    {
        public string authority { get; set; }
        public string role { get; set; }
        public bool? remove { get; set; }
    }
}
