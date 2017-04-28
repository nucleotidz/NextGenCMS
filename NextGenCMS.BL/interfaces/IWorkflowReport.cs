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

        WorkflowReportResponse GetWorkflowsDueToday(string username);

        WorkflowReportResponse GetWorkflowsDueTomorrow(string username);

        WorkflowReportResponse GetWorkflowsDueNext7Days(string username);

        WorkflowReportResponse GetWorkflowsOverdue(string username);

        WorkflowReportResponse GetWorkflowsNoDueDate(string username);

        WorkflowReportResponse GetWorkflowsStartedinLast7days(string username);

        WorkflowReportResponse GetWorkflowsStartedinLast14days(string username);

        WorkflowReportResponse GetWorkflowsStartedinLast28days(string username);

        WorkflowReportResponse GetWorkflowsHighPriority(string username);

        WorkflowReportResponse GetWorkflowsMediumPriority(string username);

        WorkflowReportResponse GetWorkflowsLowPriority(string username);
    }
}
