using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EShopDbContext _dbContext;

        public CategoryController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> Get(Guid userId)
        {
            var cart = await _dbContext.Carts
                .Include(cart => c.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.Deleted);
                
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("{userId}/items/{productId}")]
        public async Task<ActionResult<Cart>> AddItem(Guid userId, int productId, [FromQuery] int amount = 1)
        {
            if (amount <= 0)
                return BadRequest("Can't add 0 of a product.");

            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.Deleted);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _dbContext.Carts.Add(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Amount += amount;
            }
            else
            {
                cart.Items.Add(new CartProduct { CartUserId = userId, ProductId = productId, Amount = amount });
            }

            cart.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(cart);
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

