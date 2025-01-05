using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepo _expenseRepo;

        public ExpenseService(IExpenseRepo expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }

        public ApiResponse<List<ExpenseResponse>> ExpenseList()
        {
            var result = _expenseRepo.GetAll(true, "Category");
            return new ApiResponse<List<ExpenseResponse>>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<long> Create(ExpenseRequest expenseInput)
        {
            long id = _expenseRepo.Add(expenseInput);
            if (id != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<ExpenseResponse> GetExpense(long id)
        {
            var result = _expenseRepo.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<ExpenseResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<Domain.Entities.Expense> Update(ExpenseRequest expenseInput)
        {
            var res = _expenseRepo.UpdateEntity(expenseInput);
            if (res != null)
                return new ApiResponse<Domain.Entities.Expense>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<Domain.Entities.Expense>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }
    }
}
