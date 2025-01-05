namespace EMaster.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!; 
        public ICollection<Income>? Incomes { get; set; } 
        public ICollection<Expense>? Expenses { get; set; }
    }

}
