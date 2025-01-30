namespace EMaster.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!; 
        public ICollection<Income>? Incomes { get; set; } 
        public ICollection<Expense>? Expenses { get; set; }
        public Company Company { get; set; } = null!;
    }

}
