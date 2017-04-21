using NextGenCMS.Model.classes.administration;

namespace NextGenCMS.Model.classes.authentication
{
    public class LoginResponse
    {
        public string Ticket { get; set; }

        public User User { get; set; }
    }
}
