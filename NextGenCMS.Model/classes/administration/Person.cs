using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.administration
{
    public class Company
    {
        public string organization { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string postcode { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string skypeId { get; set; }
        public string googleId { get; set; }
        public string instantMessageId { get; set; }
        public string jobTitle { get; set; }
        public string location { get; set; }
        public Company company { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string userStatus { get; set; }
        public bool enabled { get; set; }
        public bool emailNotificationsEnabled { get; set; }
        public string password { get; set; }
        public List<string> aspectNames { get; set; }
    }
}
