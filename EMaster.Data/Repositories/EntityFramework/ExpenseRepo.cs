using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class ExpenseRepo : GenericRepo<Expense, ExpenseRequest, ExpenseResponse>, IExpenseRepo
    {
        private readonly EMasterContext dbContext;
        public ExpenseRepo(EMasterContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public decimal GetTotalExpenseAmount()
        {
           var res = dbContext.Expenses.Sum(x => x.Amount);
           return res;
        }

        public decimal MountlyExpenseAmount()
        {
            var res = dbContext.Expenses.Where(x => x.Date.Month == DateTime.Now.Month).Sum(x => x.Amount);
            return res;
        }

        public List<GetExpenseMonthlyCategoryAmount> GetExpenseMonthlyCategoryAmount()
        {
            var result = dbContext.Expenses
             .Where(e => !e.IsDeleted && e.Date.Year == DateTime.Now.Year) 
             .GroupBy(e => new
             {
                 Month = e.Date.Month, 
                 e.Category.Name     
             })
             .Select(g => new
             {
                 Month = g.Key.Month,
                 Category = g.Key.Name,
                 Amount = g.Sum(e => e.Amount) 
             })
             .ToList()
             .Select(g => new GetExpenseMonthlyCategoryAmount
             {
                 Month = new DateTime(1, g.Month, 1).ToString("MMMM"), 
                 Category = g.Category,
                 Amount = g.Amount
             })
             .ToList();


            return result;
        }
    }
}
