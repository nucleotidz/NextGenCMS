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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize objects
        /// </summary>
        /// <param name="apiHelper">IAPIHelper</param>
        public WorkflowReport(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
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
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowDueToday;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetWorkflowsDueTomorrow(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowDueTomorrow;
            return GetWorkflows(uri);
        }

        public WorkflowReportResponse GetWorkflowsDueNext7Days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowDueNext7Days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsOverdue(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowOverdue;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsNoDueDate(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowNoDueDate;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast7days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowStartedinLast7days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast14days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowStartedinLast14days;
            return GetWorkflows(uri);
        }
        public WorkflowReportResponse GetWorkflowsStartedinLast28days(string username)
        {
            string initiator = username == "all" ? string.Empty : ServiceUrl.WorkflowInitiator + username;
            string uri = ServiceUrl.WorkflowReport + initiator + ServiceUrl.WorkflowStartedinLast28days;
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
