using DotCMIS.Client;
using NextGenCMS.APIHelper.interfaces;
using NextGenCMS.BL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
