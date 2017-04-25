using NextGenCMS.Model.classes.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGenCMS.BL.interfaces
{
    public interface IWorkflowBL
    {
        List<WorkFlowModel> GetAllTask();
        FRootObject GetWorkflowFile(string id);
    }
}
