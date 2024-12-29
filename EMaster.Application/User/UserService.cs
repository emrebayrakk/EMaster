using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApiResponse<long> Create(UserRequest userInput)
        {
            long id = _userRepository.Add(userInput);
            if (id != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<UserResponse> GetUser(long id)
        {
            var result = _userRepository.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<UserResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<Domain.Entities.User> Update(UserRequest userInput)
        {
            var res = _userRepository.UpdateEntity(userInput);
            if (res != null)
                return new ApiResponse<Domain.Entities.User>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<Domain.Entities.User>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }

        public ApiResponse<List<UserResponse>> UserList()
        {
            var result = _userRepository.GetAll();
            return new ApiResponse<List<UserResponse>>(true, ResultCode.Instance.Ok, "Success", result);
        }
    }
}
