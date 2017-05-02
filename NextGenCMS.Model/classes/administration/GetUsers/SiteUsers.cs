

namespace NextGenCMS.Model.classes.administration.GetUsers
{
    public class SiteUsers
    {
        public string role { get; set; }
        public bool isMemberOfGroup { get; set; }
        public Authority authority { get; set; }
        public string url { get; set; }
    }
    public class Authority
    {
        public string authorityType { get; set; }
        public string fullName { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string url { get; set; }
    }
}
