using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Expense
{
    public interface IExpenseService
    {
        ApiResponse<ExpenseResponse> Create(ExpenseRequest expenseInput);
        ApiResponse<ExpenseResponse> Update(ExpenseRequest expenseInput);
        PaginatedResponse<List<ExpenseResponse>> ExpenseList(int? companyId, int pageNumber, int pageSize, List<ExpressionFilter> filters);
        ApiResponse<ExpenseResponse> GetExpense(long id);
        ApiResponse<long> DeleteExpense(int id);
        ApiResponse<ExpenseAmountResponse> GetSalaryExpense(int companyId);
        ApiResponse<List<GetExpenseMonthlyCategoryAmount>> GetExpenseMonthlyCategory(int companyId);
        
    }
}
