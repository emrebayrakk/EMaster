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

        public ApiResponse<ExpenseResponse> Create(ExpenseRequest expenseInput)
        {
            var res = _expenseRepo.AddWithReturn(expenseInput);
            if (res != null)
                return new ApiResponse<ExpenseResponse>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<ExpenseResponse>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }

        public ApiResponse<ExpenseResponse> GetExpense(long id)
        {
            var result = _expenseRepo.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<ExpenseResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<ExpenseResponse> Update(ExpenseRequest expenseInput)
        {
            var res = _expenseRepo.UpdateWithReturn(expenseInput);
            if (res != null)
                return new ApiResponse<ExpenseResponse>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<ExpenseResponse>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }
        public ApiResponse<long> DeleteExpense(int id)
        {
            var res = _expenseRepo.Delete(id);
            if (res != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }
    }
}
