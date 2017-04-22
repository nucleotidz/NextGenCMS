using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.administration.CreateUser
{
   public class AddUserRole
    {
       public class RootObject
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

    //{
    //    "invitationType":"NOMINATED",
    //    "inviteeUserName":"swanesh",
    //    "inviteeRoleName":"SiteManager",
    //    "inviteeFirstName":"Swanesh",
    //    "inviteeLastName":"Saxena",
    //    "inviteeEmail":"ssaxena46@csc.com",
    //    "serverPath":"http://127.0.0.1:8080/share/",
    //    "acceptURL":"page/accept-invite",
    //    "rejectURL":"page/reject-invite"}
}
