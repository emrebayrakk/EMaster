namespace EMaster.Domain.Requests
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
