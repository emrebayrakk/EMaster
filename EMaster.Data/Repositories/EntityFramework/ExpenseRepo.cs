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
    }
}
