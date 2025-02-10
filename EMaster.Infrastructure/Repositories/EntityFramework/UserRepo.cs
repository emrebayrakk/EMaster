using EMaster.Infrastructure.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Infrastructure.Repositories.EntityFramework
{
    public class UserRepo : GenericRepo<User, UserRequest, UserResponse>, IUserRepo
    {
        public UserRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
