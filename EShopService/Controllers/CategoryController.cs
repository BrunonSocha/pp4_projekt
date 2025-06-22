using Microsoft.AspNetCore.Authorization;
using EShopService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EShopAbstractions;
using EShopAbstractions.Models;
using EShop.Application.Services;
// What? Why is the namespace different here? All other controllers work with EShopService.Application.Services, not EShop.Application.Services
// Issue solved automatically by VSCode recommendation, I'd never figure it out myself 

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _categoryService.GetOneAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            var userId = GetUserId();
            var created = await _categoryService.CreateAsync(category, userId);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category updated)
        {
            if (id != updated.Id)
                return BadRequest("ID doesn't exist.");

            var userId = GetUserId();
            var success = await _categoryService.UpdateAsync(id, updated, userId);
            if (!success)
                return NotFound("Can't find the category to update.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            //var userId = GetUserId();
            var success = await _categoryService.DeleteAsync(id, Guid.NewGuid());
            if (!success)
                return NotFound("Can't find the category to delete.");

            return Ok(new { message = "Category was deleted." });
        }
        

        //NO WORKY, if you insert correct Guid into card POST it throws exceptions (TODO: integration with token)
        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID doesn't exist.");

            return Guid.Parse(userIdClaim.Value);
        }
    }

}

