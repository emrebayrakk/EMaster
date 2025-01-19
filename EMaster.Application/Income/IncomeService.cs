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

        public PaginatedResponse<List<IncomeResponse>> IncomeList(int pageNumber, int pageSize, List<ExpressionFilter> filters)
        {
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

        public ApiResponse<IncomeAmountResponse> GetSalaryIncome()
        {
            var totalIncomeAmount = _incomeRepo.GetTotalIncomeAmount();
            var monthlyIncomeAmount = _incomeRepo.MountlyIncomeAmount();
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

        public ApiResponse<List<GetIncomeMonthlyCategoryAmount>> GetIncomeMonthlyCategoryAmount()
        {
            var result = _incomeRepo.GetIncomeMonthlyCategoryAmounts();
            return new ApiResponse<List<GetIncomeMonthlyCategoryAmount>>(true, ResultCode.Instance.Ok, "Success", result);
        }
    }
}
