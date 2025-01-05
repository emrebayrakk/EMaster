using EMaster.Domain.Entities;

namespace EMaster.Domain.Interfaces
{
    public interface IJwtProvider
    {
        string CreateToken(User user);
    }
}
