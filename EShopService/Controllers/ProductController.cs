using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> _products = new();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_products.Where(p => !p.Deleted));
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product updated)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (product == null)
                return NotFound();

            product.Name = updated.Name;
            product.Ean = updated.Ean;
            product.Sku = updated.Sku;
            product.Stock = updated.Stock;
            product.Price = updated.Price;
            product.Category = updated.Category;
            product.UpdatedAt = DateTime.Now;
            product.UpdatedBy = updated.UpdatedBy;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (product == null)
                return NotFound();

            product.Deleted = true;
            product.UpdatedAt = DateTime.Now;

            return Ok(new { message = "Product deleted" });
        }
    }
}
