using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Income
{
    public interface IIncomeService
    {
        ApiResponse<long> Create(IncomeRequest incomeInput);
        ApiResponse<Domain.Entities.Income> Update(IncomeRequest incomeInput);
        ApiResponse<List<IncomeResponse>> IncomeList();
        ApiResponse<IncomeResponse> GetIncome(long id);
    }
}
