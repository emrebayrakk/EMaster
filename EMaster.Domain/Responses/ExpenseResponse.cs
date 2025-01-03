﻿namespace EMaster.Domain.Responses
{
    public class ExpenseResponse
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}