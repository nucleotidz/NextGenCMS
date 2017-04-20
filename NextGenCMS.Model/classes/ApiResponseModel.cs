using NextGenCMS.Model.constants;

namespace NextGenCMS.Model.classes
{
    public class ApiResponseModel<T>
    {
        public ApiHelper.StatusCode Status { get; set; }
        public T Result { get; set; }
    }

}
