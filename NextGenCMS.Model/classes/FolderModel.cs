using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes
{
    public class FolderModel
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public bool HasChildren { get; set; }

        public string Noderef { get; set; }
    }
}
