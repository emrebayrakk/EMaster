namespace EMaster.Domain.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserResponse User { get; set; }

    }
}
