namespace EMaster.Domain.Entities
{
    public class Income : BaseEntity
    {
        public int CategoryID { get; set; }
        public int CompanyId { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 
        public string? Description { get; set; } 
        public Category Category { get; set; } = null!;
        public Company Company { get; set; } = null!;
    }

}
