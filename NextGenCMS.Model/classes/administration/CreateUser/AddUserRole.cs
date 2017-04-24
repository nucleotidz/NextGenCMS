
namespace NextGenCMS.Model.classes.administration.CreateUser
{
    public class AddUserRole
    {
        public string invitationType { get; set; }
        public string inviteeUserName { get; set; }
        public string inviteeRoleName { get; set; }
        public string inviteeFirstName { get; set; }
        public string inviteeLastName { get; set; }
        public string inviteeEmail { get; set; }
        public string serverPath { get; set; }
        public string acceptURL { get; set; }
        public string rejectURL { get; set; }
    }
}
