
namespace NextGenCMS.Model.classes.administration
{
    using System.Collections.Generic;

    public class GetGroupsResponse
    {
        public List<Group> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public long maxItems { get; set; }
        public int skipCount { get; set; }
        public int totalItems { get; set; }
        public object totalItemsRangeEnd { get; set; }
        public string confidence { get; set; }
    }
}
