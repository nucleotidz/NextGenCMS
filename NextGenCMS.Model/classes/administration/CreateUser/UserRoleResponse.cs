using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.administration.CreateUser
{
    public class UserRoleResponse
    {
        public Data data { get; set; }
    }
    public class Invitee
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
    }

    public class SentInviteDate
    {
        public string iso8601 { get; set; }
    }

    public class Data
    {
        public string inviteId { get; set; }
        public string inviteeUserName { get; set; }
        public Invitee invitee { get; set; }
        public string roleName { get; set; }
        public SentInviteDate sentInviteDate { get; set; }
        public string resourceType { get; set; }
        public string resourceName { get; set; }
        public string invitationType { get; set; }
    }
}
