using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EShopDbContext _dbContext;

        public CategoryController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = await _dbContext.Categories
                .Where(c => !c.Deleted)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category updated)
        {
            if (id != updated.Id)
                return BadRequest("Wrong ID.");

            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null || category.Deleted)
                return NotFound();

            category.Name = updated.Name;
            category.UpdatedAt = DateTime.Now;
            category.UpdatedBy = updated.UpdatedBy;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null || category.Deleted)
                return NotFound();

            category.Deleted = true;
            category.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Category deleted." });
        }
    }
}

