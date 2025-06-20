using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static readonly List<Category> _category = new();
        private static int _nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Ok(_category.Where(p => !p.Deleted));
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _category.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Post([FromBody] Category category)
        {
            category.Id = _nextId++;
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            _category.Add(category);

            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category updated)
        {
            var category = _category.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (category == null)
                return NotFound();

            category.Name = updated.Name;
            category.UpdatedAt = DateTime.Now;
            category.UpdatedBy = updated.UpdatedBy;

            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public ActionResult Delete(int id)
        {
            var category = _category.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (category == null)
                return NotFound();

            category.Deleted = true;
            category.UpdatedAt = DateTime.Now;

            return Ok(new { message = "Category deleted" });
        }
    }
}

