using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class IncomeRepo : GenericRepo<Income, IncomeRequest, IncomeResponse>, IIncomeRepo
    {
        public IncomeRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
