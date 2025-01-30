using EMaster.Domain.Entities;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Domain.Interfaces.EntityFramework
{
    public interface ICompanyRepo : IGenericRepo<Company, CompanyRequest, CompanyResponse>
    {
    }
}
