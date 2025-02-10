using EMaster.Domain.Entities;

namespace EMaster.Domain.Responses
{
    public class IncomeResponse
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CategoryID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}
