using System.Collections.Generic;

namespace NextGenCMS.Model.classes.administration
{
    public class Group
    {
        public string authorityType { get; set; }
        public string itemName { get; set; }
        public string shortName { get; set; }
        public string fullName { get; set; }
        public string displayName { get; set; }
        public string url { get; set; }
        public List<string> zones { get; set; }
        public List<GroupUsers> users { get; set; }
    }

    public class GroupUsers
    {
        public string shortname  { get; set; }
        public bool removed { get; set; }
    }
}
