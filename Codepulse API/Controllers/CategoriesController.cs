using Codepulse_API.Data;
using Codepulse_API.Models.Domain;
using Codepulse_API.Models.DTO;
using Codepulse_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codepulse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] ClreateCategoryRequestDto request)
        {
            // Map DTO to Domain Model
            var Clategory = new Clategory
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await categoryRepository.CreateAsync(Clategory);

            // Domain model to DTO
            var response = new CategoryDto
            {
                Id = Clategory.Id,
                Name = Clategory.Name,
                UrlHandle = Clategory.UrlHandle
            };

            return Ok(response);
        }


        // get:https://localhost:7188/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            // map domain model to dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }

        // get:https://localhost:7188/api/Categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,UpdateCategoryRequestDto request)
        {
            // convert dto to domain model
            var category = new Clategory
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            category= await categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            // convert domain model to dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category=await categoryRepository.DeleteAsync(id);
            if(category is null)
            {
                return NotFound();
            }

            // convert to domain model
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }
    }
}
