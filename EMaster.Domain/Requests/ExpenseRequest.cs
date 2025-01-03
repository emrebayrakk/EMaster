namespace EMaster.Domain.Requests
{
    public class ExpenseRequest
    {
        public int CategoryID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
