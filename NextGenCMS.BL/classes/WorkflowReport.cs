using System;
using NextGenCMS.BL.interfaces;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.Model.classes.WorkflowReport;
using System.Web;
using NextGenCMS.Model.constants;
using Newtonsoft.Json;

namespace NextGenCMS.BL.classes
{
    public class WorkflowReport : IWorkflowReport
    {
        #region "Private fields"
        /// <summary>
        /// disposed is used to reallocate memory of Unused Objects
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;

        private DateTime _todaysDate;
        private string isoFormat;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize objects
        /// </summary>
        /// <param name="apiHelper">IAPIHelper</param>
        public WorkflowReport(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
            _todaysDate = DateTime.Today;
            isoFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";
        }
        #endregion

        #region "Public Methods"
        public WorkflowReportResponse GetAllWorkflows(string username)
        {
            string uri = string.Empty;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetActiveWorkflows(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetCompletedWorkflows(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowCompleted;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetWorkflowsDueToday(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime yesterdaysDate = new DateTime(_todaysDate.AddDays(-1).Year, _todaysDate.AddDays(-1).Month, _todaysDate.AddDays(-1).Day, 11, 59, 59, 999);
            DateTime todaysDate = new DateTime(_todaysDate.Year, _todaysDate.Month, _todaysDate.Day, 11, 59, 59, 999);
            string workflowDueToday = ServiceUrl.WorkflowDueAfter + yesterdaysDate.ToString(isoFormat) + ServiceUrl.WorkflowDueBefore + todaysDate.ToString(isoFormat);

            string uri = ServiceUrl.WorkflowReport + initiator + workflowDueToday;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetWorkflowsDueTomorrow(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime todaysDate = new DateTime(_todaysDate.Year, _todaysDate.Month, _todaysDate.Day, 11, 59, 59, 999);
            DateTime tomorrowsDate = new DateTime(_todaysDate.AddDays(1).Year, _todaysDate.AddDays(1).Month, _todaysDate.AddDays(1).Day, 11, 59, 59, 999);
            string workflowDueTomorrow = ServiceUrl.WorkflowDueAfter + todaysDate.ToString(isoFormat) + ServiceUrl.WorkflowDueBefore + tomorrowsDate.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowDueTomorrow;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetWorkflowsDueNext7Days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime todaysDate = new DateTime(_todaysDate.Year, _todaysDate.Month, _todaysDate.Day, 11, 59, 59, 999);
            DateTime next7Days = new DateTime(_todaysDate.AddDays(8).Year, _todaysDate.AddDays(8).Month, _todaysDate.AddDays(8).Day, 11, 59, 59, 999);
            string workflowDueNext7Days = ServiceUrl.WorkflowDueAfter + todaysDate.ToString(isoFormat) + ServiceUrl.WorkflowDueBefore + next7Days.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowDueNext7Days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsOverdue(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime yesterdaysDate = new DateTime(_todaysDate.AddDays(-1).Year, _todaysDate.AddDays(-1).Month, _todaysDate.AddDays(-1).Day, 11, 59, 59, 999);
            string workflowOverdue = ServiceUrl.WorkflowDueBefore + yesterdaysDate.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowOverdue;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsNoDueDate(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string workflowNoDueDate = ServiceUrl.WorkflowDueBefore + "null";
            string uri = ServiceUrl.WorkflowReport + initiator + workflowNoDueDate;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast7days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime last7Days = new DateTime(_todaysDate.AddDays(-7).Year, _todaysDate.AddDays(-7).Month, _todaysDate.AddDays(-7).Day, 11, 59, 59, 999);
            string workflowStartedinLast7days = ServiceUrl.WorkflowStartedAfter + last7Days.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowStartedinLast7days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast14days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime last14Days = new DateTime(_todaysDate.AddDays(-14).Year, _todaysDate.AddDays(-14).Month, _todaysDate.AddDays(-14).Day, 11, 59, 59, 999);
            string workflowStartedinLast14days = ServiceUrl.WorkflowStartedAfter + last14Days.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowStartedinLast14days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast28days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            DateTime last28Days = new DateTime(_todaysDate.AddDays(-28).Year, _todaysDate.AddDays(-28).Month, _todaysDate.AddDays(-28).Day, 11, 59, 59, 999);
            string workflowStartedinLast28days = ServiceUrl.WorkflowStartedAfter + last28Days.ToString(isoFormat);
            string uri = ServiceUrl.WorkflowReport + initiator + workflowStartedinLast28days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsHighPriority(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowHighPriority;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsMediumPriority(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowMediumPriority;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsLowPriority(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowLowPriority;
            return GetWorkflows(uri);
        }
        #endregion

        #region "Private Methods"
        private WorkflowReportResponse GetWorkflows(string uri)
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(uri + "&alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }

            WorkflowReportResponse response = JsonConvert.DeserializeObject<WorkflowReportResponse>(data);
            return response;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overriding Dispose method
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _apiHelper.Dispose();
                }
            }

            this._disposed = true;
        }
        #endregion
    }
}
