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
    }
}
