using EMaster.Domain.Interfaces;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepository;
        private readonly ICompanyRepo _companyRepo;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepo userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher, ICompanyRepo companyRepo)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _companyRepo = companyRepo;
        }

        public ApiResponse<long> Create(RegisterRequest request)
        {
            var existUser = _userRepository.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existUser != null)
                return new ApiResponse<long>(false, ResultCode.Instance.Duplicate, "EmailExist", -1);

            request.PasswordHash = _passwordHasher.Encrypt(request.PasswordHash);

            var companyReq = new CompanyRequest
            {
                Name = request.CompanyName
            };
            var res = _companyRepo.AddEntity(companyReq);
            if(res != null)
            {
                var userReq = new UserRequest
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = request.PasswordHash,
                    Username = request.Username,
                    CompanyId = res.Id,
                };
                long id = _userRepository.Add(userReq);
                if (id != -1)
                    return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", id);
                return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
            }
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<UserResponse> GetUser(long id)
        {
            var result = _userRepository.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<UserResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<LoginResponse> Login(LoginRequest login)
        {
            var result = _userRepository.FirstOrDefault(a => a.Email == login.Email,true,a=>a.Company);
            var passwordVerify = _passwordHasher.Verify(login.Password, result.PasswordHash);
            if (result is not null && passwordVerify)
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
                        CompanyId = result.CompanyId,
                        CompanyName = result.Company.Name
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
