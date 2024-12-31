using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace EMaster.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepo userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public ApiResponse<long> Create(UserRequest userInput)
        {
            var existUser = _userRepository.FirstOrDefaultAsync(x => x.Email == userInput.Email);
            if (existUser != null)
                return new ApiResponse<long>(false, ResultCode.Instance.Duplicate, "EmailExist", -1);

            userInput.PasswordHash = _passwordHasher.Hash(userInput.PasswordHash);
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

        public ApiResponse<LoginResponse> Login(LoginRequest login)
        {
            var result = _userRepository.FirstOrDefault(a => a.Email == login.Email);
            if (result is not null && _passwordHasher.Verify(login.Password, result.PasswordHash))
            {
                var token = _jwtProvider.CreateToken(result);
                LoginResponse response = new LoginResponse
                {
                    User = new UserResponse
                    {
                        Email = result.Email,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Id = result.Id,
                        Username = result.Username,
                    },
                    Token = token
                };
                return new ApiResponse<LoginResponse>(true, ResultCode.Instance.Ok, "Success", response);
            }
            return new ApiResponse<LoginResponse>(false, ResultCode.Instance.LoginInvalid, "LoginInvalid", null);
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
