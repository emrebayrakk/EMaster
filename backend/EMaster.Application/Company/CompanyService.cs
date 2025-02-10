using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Responses;

namespace EMaster.Application.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepo _companyRepo;

        public CompanyService(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }
    }
}
