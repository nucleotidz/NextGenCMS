using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes
{
    public class LoginToken
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public string ticket { get; set; }
    }
}
