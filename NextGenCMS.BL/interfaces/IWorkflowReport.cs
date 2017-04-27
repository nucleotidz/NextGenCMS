using NextGenCMS.Model.classes.WorkflowReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.BL.interfaces
{
    public interface IWorkflowReport : IDisposable
    {
        WorkflowReportResponse GetAllWorkflows(string username);

        WorkflowReportResponse GetActiveWorkflows(string username);

        WorkflowReportResponse GetCompletedWorkflows(string username);
    }
}
