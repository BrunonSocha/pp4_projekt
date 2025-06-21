using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShop.Abstractions;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EShopDbContext _dbContext;

        public ProductController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _dbContext.Products
                .Where(p => !p.Deleted)
                .Include(p => p.Category)
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);


            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            if (!await _dbContext.Categories.AnyAsync(c => c.Id == product.CategoryId && !c.Deleted))
                return BadRequest("Invalid CategoryId");

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product updated)
        {
            if (id != updated.Id)
                return BadRequest("Wrong ID.");
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null || product.Deleted)
                return NotFound();

            if (!await _dbContext.Categories.AnyAsync(c => c.Id == updated.CategoryId && !c.Deleted))
                return BadRequest("Invalid CategoryId");

            product.Name = updated.Name;
            product.Ean = updated.Ean;
            product.Sku = updated.Sku;
            product.Stock = updated.Stock;
            product.Price = updated.Price;
            product.CategoryId = updated.CategoryId;
            product.UpdatedAt = DateTime.Now;
            product.UpdatedBy = updated.UpdatedBy;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
            if (product == null)
                return NotFound();

            product.Deleted = true;
            product.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product deleted." });
        }
    }
}
