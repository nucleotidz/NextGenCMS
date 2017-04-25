using System.Collections.Generic;

namespace NextGenCMS.Model.classes.administration.GetUsers
{
    public class GetUsersResponse
    {
        public List<UserWithGroups> people { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public int maxItems { get; set; }
        public int totalItems { get; set; }
        public int skipCount { get; set; }
    }
}
