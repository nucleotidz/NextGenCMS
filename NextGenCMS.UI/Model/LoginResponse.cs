
using System.Collections.Generic;
namespace NextGenCMS.UI.Model
{
    public class LoginResponse
    {
        public string Ticket { get; set; }

        public User User { get; set; }

        public UserSites UserSites { get; set; }
    }
}
