using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class Pagination
    {
        public int count { get; set; }
        public bool hasMoreItems { get; set; }
        public int totalItems { get; set; }
        public int skipCount { get; set; }
        public int maxItems { get; set; }
    }

    public class Entry2
    {
        public string createdAt { get; set; }
        public int size { get; set; }
        public string createdBy { get; set; }
        public string modifiedAt { get; set; }
        public string name { get; set; }
        public string modifiedBy { get; set; }
        public string id { get; set; }
        public string mimeType { get; set; }
    }

    public class Entry
    {
        public Entry2 entry { get; set; }
    }

    public class List
    {
        public Pagination pagination { get; set; }
        public List<Entry> entries { get; set; }
    }

    public class FRootObject
    {
        public List list { get; set; }
    }
}
