using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShop.Abstractions;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EShopDbContext _dbContext;

        public CartController(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<Cart>> Get(int cartId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);
                
            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("{cartId}/items/{productId}")]
        public async Task<ActionResult<Cart>> AddItem(int cartId, int productId, [FromQuery] int amount = 1)
        {
            if (amount <= 0)
                return BadRequest("Can't add 0 of a product.");

            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

            if (cart == null)
            {
                cart = new Cart { };
                _dbContext.Carts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }

            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);

            if (product == null)
                return NotFound("Product doesn't exist.");

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Amount += amount;
            }
            else
            {
                cart.Items.Add(new CartProduct { CartId = cart.Id, ProductId = productId, Amount = amount });
            }

            cart.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return Ok(cart);
        }

        [HttpDelete("{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(int cartId, int productId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

            if (cart == null)
                return NotFound("Cart doesn't exist.");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                return NotFound("Product not found in the cart.");

            cart.Items.Remove(item);
            cart.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product removed from cart." });
            
        }


        [HttpDelete("{cartId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var cart = await _dbContext.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

            
            if (cart == null)
                return NotFound();

            cart.Deleted = true;
            cart.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Cart deleted."});
        }
    }
}

