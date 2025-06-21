using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using EShopService.Application;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> Get(int cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        [HttpPost("{cartId}/items/{productId}")]
        public async Task<IActionResult> AddItem(int cartId, int productId, [FromQuery] int amount = 1)
        {
            if (amount <= 0)
                return BadRequest("Can't add 0 of a product.");
            try
            {
                var cart = await _cartService.AddItemAsync(cartId, productId, amount);
                return Ok(cart);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(int cartId, int productId)
        {
            var success = await _cartService.RemoveItemAsync(cartId, productId);
            if (!success)
                return NotFound();
            return Ok(new { message = "Product deleted." });
        }


        [HttpDelete("{cartId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            var success = await _cartService.DeleteCartAsync(cartId);
            if (!success)
                return NotFound();

            return Ok(new { message = "Cart deleted." });
        }
    }
}

