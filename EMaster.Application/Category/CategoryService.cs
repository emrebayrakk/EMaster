using EMaster.Domain.Interfaces.EntityFramework;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;

namespace EMaster.Application.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public ApiResponse<List<CategoryResponse>> CategoryList()
        {
            var result = _categoryRepo.GetAll();
            return new ApiResponse<List<CategoryResponse>>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<CategoryResponse> Create(CategoryRequest categoyInput)
        {
            var res = _categoryRepo.AddWithReturn(categoyInput);
            if (res != null)
                return new ApiResponse<CategoryResponse>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<CategoryResponse>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }

        public ApiResponse<long> DeleteCategory(int id)
        {
            var res = _categoryRepo.Delete(id);
            if (res != -1)
                return new ApiResponse<long>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<long>(false, ResultCode.Instance.Failed, "ErrorOccured", -1);
        }

        public ApiResponse<CategoryResponse> GetCategory(long id)
        {
            var result = _categoryRepo.FirstOrDefaultAsync(x => x.Id == id);
            return new ApiResponse<CategoryResponse>(true, ResultCode.Instance.Ok, "Success", result);
        }

        public ApiResponse<Domain.Entities.Category> Update(CategoryRequest categoyInput)
        {
            var res = _categoryRepo.UpdateEntity(categoyInput);
            if (res != null)
                return new ApiResponse<Domain.Entities.Category>(true, ResultCode.Instance.Ok, "Success", res);
            return new ApiResponse<Domain.Entities.Category>(false, ResultCode.Instance.Failed, "ErrorOccured", null);
        }
    }
}
