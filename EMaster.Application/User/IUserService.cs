using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.User
{
    public interface IUserService
    {
        ApiResponse<long> Create(UserRequest userInput);
        ApiResponse<Domain.Entities.User> Update(UserRequest userInput);
        ApiResponse<List<UserResponse>> UserList();
        ApiResponse<UserResponse> GetUser(long id);
        ApiResponse<LoginResponse> Login(LoginRequest login);
    }
}
