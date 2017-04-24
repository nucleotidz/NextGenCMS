using DotCMIS.Client;
using Newtonsoft.Json;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using NextGenCMS.Model.classes.Workflow;
using NextGenCMS.Model.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NextGenCMS.BL.classes
{
    public class WorkflowBL : IWorkflowBL
    {
        /// <summary>
        /// disposed is used to reallocate memory of UnUsed Objects
        /// </summary>
        private bool _disposed;

        private ISession session = null;

        /// <summary>
        /// api helper object
        /// </summary>
        private readonly IAPIHelper _apiHelper;

        public WorkflowBL(IAPIHelper apiHelper)
        {
            this._apiHelper = apiHelper;
        }

        public RootObject GetAllTask()
        {
            string data = string.Empty;
            if (HttpContext.Current.Items[Filter.Token] != null)
            {
                data = this._apiHelper.Get(ServiceUrl.SubFolder + "?alf_ticket=" + HttpContext.Current.Items[Filter.Token]);
            }
            return JsonConvert.DeserializeObject<RootObject>(data);
        }
    }
}
