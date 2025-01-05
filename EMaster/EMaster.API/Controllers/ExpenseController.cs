using EMaster.Application.Category;
using EMaster.Application.Expense;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(ApiResponse<List<ExpenseResponse>>), StatusCodes.Status200OK)]
        public ApiResponse<List<ExpenseResponse>> ExpenseList()
        {
            return _expenseService.ExpenseList();
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<Domain.Entities.Expense>), StatusCodes.Status200OK)]
        public ApiResponse<Domain.Entities.Expense> ExpenseUpdate([FromBody] ExpenseRequest expense)
        {
            return _expenseService.Update(expense);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<ExpenseResponse>), StatusCodes.Status200OK)]
        public ApiResponse<ExpenseResponse> GetExpense(long id)
        {
            return _expenseService.GetExpense(id);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Create([FromBody] ExpenseRequest expense)
        {
            var response = _expenseService.Create(expense);
            return response;
        }
    }
}
