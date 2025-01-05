namespace EMaster.Domain.Requests
{
    public class ExpenseRequest
    {
        public int CategoryID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Description { get; set; }
    }
}
