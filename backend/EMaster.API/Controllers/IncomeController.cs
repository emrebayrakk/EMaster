using EMaster.Application.Income;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost("List")]
        [ProducesResponseType(typeof(ApiResponse<List<IncomeResponse>>), StatusCodes.Status200OK)]
        public PaginatedResponse<List<IncomeResponse>> IncomeList([FromBody] PaginatedRequest request)
        {
            return _incomeService.IncomeList(request.companyId,request.pageNumber, request.pageSize, request.filters);
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<IncomeResponse>), StatusCodes.Status200OK)]
        public ApiResponse<IncomeResponse> IncomeUpdate([FromBody] IncomeRequest income)
        {
            return _incomeService.Update(income);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<IncomeResponse>), StatusCodes.Status200OK)]
        public ApiResponse<IncomeResponse> GetIncome(long id)
        {
            return _incomeService.GetIncome(id);
        }
        [HttpPost("GetSalary")]
        [ProducesResponseType(typeof(ApiResponse<IncomeAmountResponse>), StatusCodes.Status200OK)]
        public ApiResponse<IncomeAmountResponse> GetSalary([FromBody] CompanyByIdRequest req)
        {
            return _incomeService.GetSalaryIncome(req.companyId);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<IncomeResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<IncomeResponse>), StatusCodes.Status404NotFound)]
        public ApiResponse<IncomeResponse> Create([FromBody] IncomeRequest income)
        {
            var response = _incomeService.Create(income);
            return response;
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Delete(int id)
        {
            var response = _incomeService.DeleteIncome(id);
            return response;
        }
        [AllowAnonymous]
        [HttpPost("GetIncomeMonthlyCategory")]
        [ProducesResponseType(typeof(ApiResponse<GetIncomeMonthlyCategoryAmount>), StatusCodes.Status200OK)]
        public ApiResponse<List<GetIncomeMonthlyCategoryAmount>> GetIncomeMonthlyCategory([FromBody] CompanyByIdRequest req)
        {
            return _incomeService.GetIncomeMonthlyCategoryAmount(req.companyId);
        }
    }
}
