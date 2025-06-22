using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EShopAbstractions;
using EShopAbstractions.Models;
using EShopService.Application.Services;
// And here the standard ref works for some reason. No idea what's going on.

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetOneAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            var userId = GetUserId();
            var created = await _productService.CreateAsync(product, userId);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product updated)
        {
            if (id != updated.Id)
                return BadRequest("Wrong ID.");
            var userId = GetUserId();
            var success = await _productService.UpdateAsync(id, updated, userId);
            if (!success)
                return NotFound("Can't find the product to update.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var success = await _productService.DeleteAsync(id, userId);
            if (!success)
                return NotFound("Can't find the product to delete.");
            return Ok(new { message = "Product deleted." });
        }

        private Guid GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("ID doesn't exist.");
            return Guid.Parse(claim.Value);
        }
    }
}
