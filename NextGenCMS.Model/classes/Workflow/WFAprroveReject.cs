
namespace NextGenCMS.Model.classes.Workflow
{
    public class WFAprroveReject
    {
        public string prop_wf_reviewOutcome { get; set; }
        public string prop_bpm_comment { get; set; }
        public string prop_transitions { get; set; }
        public string prop_bpm_status { get; set; }
    }

    public class WFApproveRejectModel
    {
        public string activitiid { get; set; }
        public WFAprroveReject WFAprroveReject { get; set; }
    }
}
