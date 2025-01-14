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

        public ApiResponse<List<IncomeResponse>> IncomeList()
        {
            var result = _incomeRepo.GetAll(true, "Category");
            return new ApiResponse<List<IncomeResponse>>(true, ResultCode.Instance.Ok, "Success", result);
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
    }
}
