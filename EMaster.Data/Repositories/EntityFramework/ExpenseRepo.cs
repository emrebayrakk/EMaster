using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class ExpenseRepo : GenericRepo<Expense, ExpenseRequest, ExpenseResponse>, IExpenseRepo
    {
        public ExpenseRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
