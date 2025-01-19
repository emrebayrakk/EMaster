using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Expense
{
    public interface IExpenseService
    {
        ApiResponse<ExpenseResponse> Create(ExpenseRequest expenseInput);
        ApiResponse<ExpenseResponse> Update(ExpenseRequest expenseInput);
        PaginatedResponse<List<ExpenseResponse>> ExpenseList(int pageNumber, int pageSize, List<ExpressionFilter> filters);
        ApiResponse<ExpenseResponse> GetExpense(long id);
        ApiResponse<long> DeleteExpense(int id);
        ApiResponse<ExpenseAmountResponse> GetSalaryExpense();
        ApiResponse<List<GetExpenseMonthlyCategoryAmount>> GetExpenseMonthlyCategory();
        
    }
}
