using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Category
{
    public interface ICategoryService
    {
        ApiResponse<CategoryResponse> Create(CategoryRequest categoyInput);
        ApiResponse<Domain.Entities.Category> Update(CategoryRequest categoyInput);
        PaginatedResponse<List<CategoryResponse>> CategoryList(int? companyId, int pageNumber, int pageSize, List<ExpressionFilter>? filters);
        ApiResponse<CategoryResponse> GetCategory(long id);
        ApiResponse<long> DeleteCategory(int id);
    }
}
