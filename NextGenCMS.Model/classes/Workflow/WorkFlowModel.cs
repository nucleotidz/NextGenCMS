﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.Model.classes.Workflow
{
    public class WorkFlowModel
    {

        public string pid { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string state { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public string Activityid { get; set; }
        public string outcome { get; set; }
        public string OwnerUsername { get; set; }
        public string fullName { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public string taskId { get; set; }
        public string priority { get; set; }
        public string workflowid { get; set; }
        public string description { get; set; }
        public string creatorUserName { get; set; }
        public string cm_name { get; set; }
    }
}
