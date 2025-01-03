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

        public ApiResponse<long> Create(IncomeRequest incomeInput)
        {
            long id = _incomeRepo.Add(incomeInput);
            if (id != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<IncomeResponse> GetIncome(long id)
        {
            var result = _incomeRepo.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<IncomeResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<Domain.Entities.Income> Update(IncomeRequest incomeInput)
        {
            var res = _incomeRepo.UpdateEntity(incomeInput);
            if (res != null)
                return new ApiResponse<Domain.Entities.Income>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<Domain.Entities.Income>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }
    }
}
