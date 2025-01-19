using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Income
{
    public interface IIncomeService
    {
        ApiResponse<IncomeResponse> Create(IncomeRequest incomeInput);
        ApiResponse<IncomeResponse> Update(IncomeRequest incomeInput);
        PaginatedResponse<List<IncomeResponse>> IncomeList(int pageNumber, int pageSize, List<ExpressionFilter> filters);
        ApiResponse<IncomeResponse> GetIncome(long id);
        ApiResponse<IncomeAmountResponse> GetSalaryIncome();
        ApiResponse<long> DeleteIncome(int id);
        ApiResponse<List<GetIncomeMonthlyCategoryAmount>> GetIncomeMonthlyCategoryAmount();
    }
}
