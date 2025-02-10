using EMaster.Infrastructure.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Infrastructure.Repositories.EntityFramework
{
    public class CategoryRepo : GenericRepo<Category, CategoryRequest, CategoryResponse>, ICategoryRepo
    {
        public CategoryRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
