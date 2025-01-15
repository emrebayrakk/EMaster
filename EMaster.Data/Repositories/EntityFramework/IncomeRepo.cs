using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class IncomeRepo : GenericRepo<Income, IncomeRequest, IncomeResponse>, IIncomeRepo
    {
        private readonly EMasterContext dbContext;
        public IncomeRepo(EMasterContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public decimal GetTotalIncomeAmount()
        {
            var res = dbContext.Incomes.Sum(x => x.Amount);
            return res;
        }

        public decimal MountlyIncomeAmount()
        {
            var res = dbContext.Incomes.Where(x => x.Date.Month == DateTime.Now.Month).Sum(x => x.Amount);
            return res;
        }

        public List<GetIncomeMonthlyCategoryAmount> GetIncomeMonthlyCategoryAmounts()
        {
            var result = dbContext.Incomes
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
             .Select(g => new GetIncomeMonthlyCategoryAmount
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
