namespace EMaster.Domain.Responses
{
    public class GetIncomeMonthlyCategoryAmount
    {
        public string Month { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
