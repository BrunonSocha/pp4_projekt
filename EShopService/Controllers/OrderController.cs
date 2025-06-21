//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using EShop.Abstractions;

//namespace EShopService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderController : ControllerBase
//    {
//        private readonly EShopDbContext _dbContext;

//        public OrderController(EShopDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Order>> GetOrder(int id)
//        {
//            var order = await _dbContext.Orders
//                .Include(o => o.Cart)
//                    .ThenInclude(c => c.Items)
//                    .ThenInclude(cp => cp.Product)
//                .FirstOrDefaultAsync(o => o.Id == id);

//            if (order == null)
//                return NotFound();

//            return Ok(order);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request)
//        {
//            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId);
//            if (user == null)
//                return BadRequest("User doesn't exist.");

//            var cart = await _dbContext.Carts
//                .Include(c => c.Items)
//                .FirstOrDefaultAsync(c => c.Id == request.CartId && !c.Deleted);

//            if (cart == null)
//                return BadRequest("Cart doesn't exist or has been deleted.");

//            var order = new Order { CartId = cart.Id, CreatedBy = user.UserId, CreatedAt = DateTime.Now };
//            cart.Deleted = true;
//            cart.UpdatedAt = DateTime.Now;

//            _dbContext.Orders.Add(order);
//            await _dbContext.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
//        }
//    }

//    public class CreateOrderRequest
//    {
//        public Guid UserId { get; set; }
        
//        public int CartId { get; set; }
//    }
//}