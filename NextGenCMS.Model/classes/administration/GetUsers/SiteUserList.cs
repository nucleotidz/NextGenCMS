using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.administration.GetUsers
{
    public class SiteUserList
    {
        public UserList UserList { get; set; }
    }
    public class Pagination
    {
        public int count { get; set; }
        public bool hasMoreItems { get; set; }
        public int skipCount { get; set; }
        public int maxItems { get; set; }
    }

    public class Entry2
    {
        public string role { get; set; }
        public Person person { get; set; }
        public string id { get; set; }
    }

    public class Persons
    {
        public Entry2 entry { get; set; }
    }

    public class UserList
    {
        public Pagination pagination { get; set; }
        public List<Persons> Persons { get; set; }
    }
}
