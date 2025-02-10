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

        public PaginatedResponse<List<ExpenseResponse>> ExpenseList(int? companyId, int pageNumber, int pageSize, List<ExpressionFilter> filters)
        {

            if (companyId == null || companyId == 0)
            {
                return new PaginatedResponse<List<ExpenseResponse>>(false, 404, "Company not found", 0, pageNumber, pageSize, null);
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
            var result = _expenseRepo.GetPaginatedDataWithFilter(pageNumber,pageSize,filters,"Category");
            return result;
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

        public ApiResponse<ExpenseAmountResponse> GetSalaryExpense(int companyId)
        {
            var totalExpenseAmount = _expenseRepo.GetTotalExpenseAmount(companyId);
            var monthlyExpenseAmount = _expenseRepo.MountlyExpenseAmount(companyId);
            var res = new ExpenseAmountResponse(totalExpenseAmount, monthlyExpenseAmount);
            return new ApiResponse<ExpenseAmountResponse>(true, ResultCode.Instance.Ok, "Success", res);
        }

        public ApiResponse<List<GetExpenseMonthlyCategoryAmount>> GetExpenseMonthlyCategory(int companyId)
        {
            var result = _expenseRepo.GetExpenseMonthlyCategoryAmount(companyId);
            return new ApiResponse<List<GetExpenseMonthlyCategoryAmount>>(true, ResultCode.Instance.Ok, "Success", result);
        }
    }
}
