using EMaster.Application.User;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{

    [Route("EMaster/api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("List")]
        [ProducesResponseType(typeof(ApiResponse<List<UserResponse>>), StatusCodes.Status200OK)]
        public ApiResponse<List<UserResponse>> UserList()
        {
            return _userService.UserList();
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<Domain.Entities.User>), StatusCodes.Status200OK)]
        public ApiResponse<Domain.Entities.User> UserUpdate([FromBody] UserRequest user)
        {
            return _userService.Update(user);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
        public ApiResponse<UserResponse> GetUser(long id)
        {
            return _userService.GetUser(id);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Create([FromBody] UserRequest user)
        {
            var response = _userService.Create(user);
            return response;
        }
    }
}
