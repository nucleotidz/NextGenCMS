
namespace NextGenCMS.Model.classes.administration
{
    public class Paging
    {
        public long maxItems { get; set; }
        public int skipCount { get; set; }
        public int totalItems { get; set; }
        public object totalItemsRangeEnd { get; set; }
        public string confidence { get; set; }
    }
}
