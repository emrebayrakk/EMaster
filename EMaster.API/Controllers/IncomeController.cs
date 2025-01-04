using EMaster.Application.Income;
using EMaster.Application.Income;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(ApiResponse<List<IncomeResponse>>), StatusCodes.Status200OK)]
        public ApiResponse<List<IncomeResponse>> IncomeList()
        {
            return _incomeService.IncomeList();
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<Domain.Entities.Income>), StatusCodes.Status200OK)]
        public ApiResponse<Domain.Entities.Income> IncomeUpdate([FromBody] IncomeRequest income)
        {
            return _incomeService.Update(income);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<IncomeResponse>), StatusCodes.Status200OK)]
        public ApiResponse<IncomeResponse> GetIncome(long id)
        {
            return _incomeService.GetIncome(id);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Create([FromBody] IncomeRequest income)
        {
            var response = _incomeService.Create(income);
            return response;
        }
    }
}
