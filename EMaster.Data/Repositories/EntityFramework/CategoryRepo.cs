using EMaster.Data.Context;
using EMaster.Domain.Entities;
using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Data.Repositories.EntityFramework
{
    public class CategoryRepo : GenericRepo<Category, CategoryRequest, CategoryResponse>, ICategoryRepo
    {
        public CategoryRepo(EMasterContext dbContext) : base(dbContext)
        {
        }
    }
}
