using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Category
{
    public interface ICategoryService
    {
        ApiResponse<long> Create(CategoryRequest categoyInput);
        ApiResponse<Domain.Entities.Category> Update(CategoryRequest categoyInput);
        ApiResponse<List<CategoryResponse>> CategoryList();
        ApiResponse<CategoryResponse> GetCategory(long id);
        ApiResponse<long> DeleteCategory(int id);
    }
}
