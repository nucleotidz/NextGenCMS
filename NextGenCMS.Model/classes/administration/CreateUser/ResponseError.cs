using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.administration.CreateUser
{
    public class ResponseError
    {
        public Status status { get; set; }
        public string message { get; set; }
        public string exception { get; set; }
        public List<object> callstack { get; set; }
        public string server { get; set; }
        public string time { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
