using NextGenCMS.Model.classes.administration;
using System.Collections.Generic;

namespace NextGenCMS.Model.classes.authentication
{
    public class LoginResponse
    {
        public string Ticket { get; set; }

        public UserWithGroups User { get; set; }

        public UserSites UserSites { get; set; }
    }
}
