using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Search
{
    public class SearchResultModel
    {
        public int totalRecords { get; set; }
        public int totalRecordsUpper { get; set; }
        public int startIndex { get; set; }
        public int numberFound { get; set; }
        public List<SearchItemModel> items { get; set; }
    }
}
