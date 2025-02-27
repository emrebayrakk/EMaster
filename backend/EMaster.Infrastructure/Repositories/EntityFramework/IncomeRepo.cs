﻿using EMaster.Infrastructure.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Infrastructure.Repositories.EntityFramework
{
    public class IncomeRepo : GenericRepo<Income, IncomeRequest, IncomeResponse>, IIncomeRepo
    {
        private readonly EMasterContext dbContext;
        public IncomeRepo(EMasterContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public decimal GetTotalIncomeAmount(int companyId)
        {
            var res = dbContext.Incomes.Where(a=>a.CompanyId== companyId).Sum(x => x.Amount);
            return res;
        }

        public decimal MountlyIncomeAmount(int companyId)
        {
            var res = dbContext.Incomes.Where(x => x.Date.Month == DateTime.Now.Month && x.CompanyId == companyId).Sum(x => x.Amount);
            return res;
        }

        public List<GetIncomeMonthlyCategoryAmount> GetIncomeMonthlyCategoryAmounts(int companyId)
        {
            var result = dbContext.Incomes
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
