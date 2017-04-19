
namespace NextGenCMS.Model.classes.administration
{
    using System.Collections.Generic;

    public class GetGroupsResponse
    {
        public List<Group> data { get; set; }
        public Paging paging { get; set; }
    }
}
