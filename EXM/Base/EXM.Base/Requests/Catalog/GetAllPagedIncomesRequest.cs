namespace EXM.Base.Requests.Catalog
{
    public class GetAllPagedIncomesRequest : PagedRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string SearchString { get; set; }
    }
}
