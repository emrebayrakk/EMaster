namespace EMaster.Domain.Requests
{
    public class PaginatedRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<ExpressionFilter>? filters { get; set; }
    }
}
