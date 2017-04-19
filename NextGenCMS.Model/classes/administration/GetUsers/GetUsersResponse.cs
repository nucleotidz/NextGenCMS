using System.Collections.Generic;

namespace NextGenCMS.Model.classes.administration.GetUsers
{
    public class GetUsersResponse
    {
        public List<User> people { get; set; }
        public Paging paging { get; set; }
    }
}
