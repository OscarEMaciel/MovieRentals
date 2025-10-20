using Aplication.Models.Requests;
using Aplication.Models;
using Aplication.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery] bool includeDeleted = false)
        {
            var categories = await _categoryService.GetAllAsync(includeDeleted);

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Movies = c.Movie.Select(b => new MovieDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    CategoryName = c.Name
                }).ToList()
            });

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Cliente")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id, [FromQuery] bool includeDeleted = false)
        {
            var category = await _categoryService.GetByIdAsync(id, includeDeleted);
            if (category == null) return NotFound();

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Movies = category.Movie.Select(b => new MovieDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Stock = b.Stock,
                    CategoryName = category.Name
                }).ToList()
            };

            return Ok(categoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryRequest createDto)
        {
            var category = new Category
            {
                Name = createDto.Name
            };

            var created = await _categoryService.CreateAsync(category);

            var categoryDto = new CategoryDto
            {
                Id = created.Id,
                Name = created.Name,
                Movies = new List<MovieDto>()
            };

            return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryRequest updateDto)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            category.Name = updateDto.Name;

            var updated = await _categoryService.UpdateAsync(category);
            if (!updated) return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
