using System.Collections.Generic;

namespace NextGenCMS.Model.classes.permissions
{
    public class Authority
    {
        public string name { get; set; }
        public string authorityType { get; set; }
        public string shortName { get; set; }
        public string fullName { get; set; }
        public string displayName { get; set; }
    }

    public class Inherited
    {
        public Authority authority { get; set; }
        public string role { get; set; }
    }

    public class Direct
    {
        public Authority authority { get; set; }
        public string role { get; set; }
    }

    public class Permissions
    {
        public List<Inherited> inherited { get; set; }
        public bool isInherited { get; set; }
        public bool canReadInherited { get; set; }
        public List<Direct> direct { get; set; }
        public List<string> settable { get; set; }
    }
}
