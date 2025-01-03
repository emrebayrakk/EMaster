using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Expense
{
    public interface IExpenseService
    {
        ApiResponse<long> Create(ExpenseRequest expenseInput);
        ApiResponse<Domain.Entities.Expense> Update(ExpenseRequest expenseInput);
        ApiResponse<List<ExpenseResponse>> ExpenseList();
        ApiResponse<ExpenseResponse> GetExpense(long id);
    }
}
