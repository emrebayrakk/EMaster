using EMaster.Application.Category;
using EMaster.Application.Expense;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [AllowAnonymous]
        [HttpPost("List")]
        [ProducesResponseType(typeof(PaginatedResponse<List<ExpenseResponse>>), StatusCodes.Status200OK)]
        public PaginatedResponse<List<ExpenseResponse>> ExpenseList([FromBody] PaginatedRequest request)
        {
            return _expenseService.ExpenseList(request.companyId,request.pageNumber, request.pageSize, request.filters);
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<ExpenseResponse>), StatusCodes.Status200OK)]
        public ApiResponse<ExpenseResponse> ExpenseUpdate([FromBody] ExpenseRequest expense)
        {
            return _expenseService.Update(expense);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<ExpenseResponse>), StatusCodes.Status200OK)]
        public ApiResponse<ExpenseResponse> GetExpense(long id)
        {
            return _expenseService.GetExpense(id);
        }
        [AllowAnonymous]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<ExpenseResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<ExpenseResponse>), StatusCodes.Status404NotFound)]
        public ApiResponse<ExpenseResponse> Create([FromBody] ExpenseRequest expense)
        {
            var response = _expenseService.Create(expense);
            return response;
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Delete(int id)
        {
            var response = _expenseService.DeleteExpense(id);
            return response;
        }
        [HttpPost("GetSalary")]
        [ProducesResponseType(typeof(ApiResponse<ExpenseAmountResponse>), StatusCodes.Status200OK)]
        public ApiResponse<ExpenseAmountResponse> GetSalary([FromBody] CompanyByIdRequest req)
        {
            return _expenseService.GetSalaryExpense(req.companyId);
        }
        [AllowAnonymous]
        [HttpPost("GetExpenseMonthlyCategory")]
        [ProducesResponseType(typeof(ApiResponse<GetExpenseMonthlyCategoryAmount>), StatusCodes.Status200OK)]
        public ApiResponse<List<GetExpenseMonthlyCategoryAmount>> GetExpenseMonthlyCategory([FromBody] CompanyByIdRequest req)
        {
            return _expenseService.GetExpenseMonthlyCategory(req.companyId);
        }
    }
}
