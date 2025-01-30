using EMaster.Domain.Entities;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Domain.Interfaces.EntityFramework
{
    public interface IIncomeRepo : IGenericRepo<Income, IncomeRequest, IncomeResponse>
    {
        decimal GetTotalIncomeAmount(int companyId);
        decimal MountlyIncomeAmount(int companyId);
        public List<GetIncomeMonthlyCategoryAmount> GetIncomeMonthlyCategoryAmounts(int companyId);
    }
}
