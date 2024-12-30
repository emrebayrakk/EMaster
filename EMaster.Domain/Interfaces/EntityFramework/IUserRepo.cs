using EMaster.Domain.Entities;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Domain.Interfaces.EntityFramework
{
    public interface IUserRepo : IGenericRepo<User,UserRequest,UserResponse>
    {
       
    }
}
