using NextGenCMS.Model.constants;

namespace NextGenCMS.Model.classes
{
    public class WebResponseModel
    {
        public ApiHelper.StatusCode status { get; set; }
        public string message { get; set; }
    }
}
