using EMaster.Infrastructure.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Infrastructure.Repositories.EntityFramework
{
    public class ExpenseRepo : GenericRepo<Expense, ExpenseRequest, ExpenseResponse>, IExpenseRepo
    {
        private readonly EMasterContext dbContext;
        public ExpenseRepo(EMasterContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public decimal GetTotalExpenseAmount(int companyId)
        {
           var res = dbContext.Expenses.Where(a=>a.CompanyId == companyId).Sum(x => x.Amount);
           return res;
        }

        public decimal MountlyExpenseAmount(int companyId)
        {
            var res = dbContext.Expenses.Where(x => x.Date.Month == DateTime.Now.Month && x.CompanyId == companyId).Sum(x => x.Amount);
            return res;
        }

        public List<GetExpenseMonthlyCategoryAmount> GetExpenseMonthlyCategoryAmount(int companyId)
        {
            var result = dbContext.Expenses
             .Where(e => !e.IsDeleted && e.Date.Year == DateTime.Now.Year && e.CompanyId == companyId) 
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
