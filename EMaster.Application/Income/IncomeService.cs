using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Income
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepo _incomeRepo;

        public IncomeService(IIncomeRepo incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }

        public PaginatedResponse<List<IncomeResponse>> IncomeList(int? companyId, int pageNumber, int pageSize, List<ExpressionFilter> filters)
        {
            if (companyId == null || companyId == 0)
            {
                return new PaginatedResponse<List<IncomeResponse>>(false, 404, "Company not found", 0, pageNumber, pageSize, null);
            }
            if (filters == null)
            {
                filters = new List<ExpressionFilter>();
            }
            filters.Add(new ExpressionFilter
            {
                PropertyName = "CompanyId",
                Comparison = Comparison.Equal,
                Value = companyId
            });
            var result = _incomeRepo.GetPaginatedDataWithFilter(pageNumber, pageSize, filters, "Category");
            return result;
        }

        public ApiResponse<IncomeResponse> Create(IncomeRequest incomeInput)
        {
            var res = _incomeRepo.AddWithReturn(incomeInput);
            if (res != null)
                return new ApiResponse<IncomeResponse>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<IncomeResponse>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }

        public ApiResponse<IncomeResponse> GetIncome(long id)
        {
            var result = _incomeRepo.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<IncomeResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<IncomeResponse> Update(IncomeRequest incomeInput)
        {
            var res = _incomeRepo.UpdateWithReturn(incomeInput);
            if (res != null)
                return new ApiResponse<IncomeResponse>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<IncomeResponse>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }

        public ApiResponse<IncomeAmountResponse> GetSalaryIncome(int companyId)
        {
            var totalIncomeAmount = _incomeRepo.GetTotalIncomeAmount(companyId);
            var monthlyIncomeAmount = _incomeRepo.MountlyIncomeAmount(companyId);
            var res = new IncomeAmountResponse(totalIncomeAmount, monthlyIncomeAmount);
            return new ApiResponse<IncomeAmountResponse>(true, ResultCode.Instance.Ok, "Success", res);
        }

        public ApiResponse<long> DeleteIncome(int id)
        {
            var res = _incomeRepo.Delete(id);
            if (res != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<List<GetIncomeMonthlyCategoryAmount>> GetIncomeMonthlyCategoryAmount(int companyId)
        {
            var result = _incomeRepo.GetIncomeMonthlyCategoryAmounts(companyId);
            return new ApiResponse<List<GetIncomeMonthlyCategoryAmount>>(true, ResultCode.Instance.Ok, "Success", result);
        }
    }
}
