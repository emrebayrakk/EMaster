using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Income
{
    public interface IIncomeService
    {
        ApiResponse<IncomeResponse> Create(IncomeRequest incomeInput);
        ApiResponse<IncomeResponse> Update(IncomeRequest incomeInput);
        ApiResponse<List<IncomeResponse>> IncomeList();
        ApiResponse<IncomeResponse> GetIncome(long id);
        ApiResponse<IncomeAmountResponse> GetSalaryIncome();
        ApiResponse<long> DeleteIncome(int id);
    }
}
