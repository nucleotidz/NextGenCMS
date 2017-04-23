using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.Alfresco.Common
{
    public class DeleteRootObject
    {
        public int totalResults { get; set; }
        public bool overallSuccess { get; set; }
        public int successCount { get; set; }
        public int failureCount { get; set; }
        public List<Result> results { get; set; }
    }
}
