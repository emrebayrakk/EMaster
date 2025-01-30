using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class CompanyRepo : GenericRepo<Company, CompanyRequest, CompanyResponse> , ICompanyRepo
    {
        public CompanyRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
