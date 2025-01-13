using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Expense
{
    public interface IExpenseService
    {
        ApiResponse<ExpenseResponse> Create(ExpenseRequest expenseInput);
        ApiResponse<ExpenseResponse> Update(ExpenseRequest expenseInput);
        ApiResponse<List<ExpenseResponse>> ExpenseList();
        ApiResponse<ExpenseResponse> GetExpense(long id);
        ApiResponse<long> DeleteExpense(int id);
    }
}
