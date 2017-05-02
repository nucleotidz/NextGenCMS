using NextGenCMS.Model.classes.administration.GetUsers;
using System.Collections.Generic;

namespace NextGenCMS.Model.classes.administration
{
    public class UserWithGroups
    {
        public string url { get; set; }
        public string userName { get; set; }
        public bool enabled { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string jobtitle { get; set; }
        public string organization { get; set; }
        public string organizationId { get; set; }
        public string location { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string companyaddress1 { get; set; }
        public string companyaddress2 { get; set; }
        public string companyaddress3 { get; set; }
        public string companypostcode { get; set; }
        public string companytelephone { get; set; }
        public string companyfax { get; set; }
        public string companyemail { get; set; }
        public string skype { get; set; }
        public string instantmsg { get; set; }
        public string userStatus { get; set; }
        public UserStatusTime userStatusTime { get; set; }
        public string googleusername { get; set; }
        public int quota { get; set; }
        public int sizeCurrent { get; set; }
        public bool emailFeedDisabled { get; set; }
        public string persondescription { get; set; }
        public string authorizationStatus { get; set; }
        public bool isDeleted { get; set; }
        public bool isAdminAuthority { get; set; }
        public Capabilities capabilities { get; set; }
        public List<Group> groups { get; set; }
        public string role { get; set; }
    }
    public class Capabilities
    {
        public bool isAdmin { get; set; }
        public bool isMutable { get; set; }
        public bool isGuest { get; set; }
    }

    public class UserStatusTime
    {
        public string iso8601 { get; set; }
    }
}
