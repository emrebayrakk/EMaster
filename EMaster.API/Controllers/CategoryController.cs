﻿using EMaster.Application.Category;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EMaster.API.Controllers
{
    [Route("EMaster/api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("List")]
        [ProducesResponseType(typeof(ApiResponse<List<CategoryResponse>>), StatusCodes.Status200OK)]
        public ApiResponse<List<CategoryResponse>> CategoryList()
        {
            return _categoryService.CategoryList();
        }
        [HttpPut("Update")]
        [ProducesResponseType(typeof(ApiResponse<Domain.Entities.Category>), StatusCodes.Status200OK)]
        public ApiResponse<Domain.Entities.Category> CategoryUpdate([FromBody] CategoryRequest category)
        {
            return _categoryService.Update(category);
        }
        [HttpGet("Get")]
        [ProducesResponseType(typeof(ApiResponse<CategoryResponse>), StatusCodes.Status200OK)]
        public ApiResponse<CategoryResponse> GetCategory(long id)
        {
            return _categoryService.GetCategory(id);
        }
        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<long>), StatusCodes.Status404NotFound)]
        public ApiResponse<long> Create([FromBody] CategoryRequest category)
        {
            var response = _categoryService.Create(category);
            return response;
        }
    }
}
