using EShopService;
using Microsoft.EntityFrameworkCore;
using EShopAbstractions;
using EShopAbstractions.Models;

namespace EShopService.Application.Services;

public class OrderService : IOrderService
{
    private readonly EShopDbContext _dbContext;

    public OrderService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object?> GetOrderAsync(int orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(cp => cp.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
            return null;
        var totalPrice = order.Cart.Items
            .Sum(item => item.Product.Price * item.Amount);

        return new { Order = order, TotalPrice = totalPrice };
    }

    public async Task<Order> CreateOrderAsync(Guid userId, int cartId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
            throw new Exception("User doesn't exist.");

        var cart = await _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

        if (cart == null)
            throw new Exception("Cart doesn't exist.");

        var order = new Order { CartId = cart.Id, UserId = user.UserId, OrderDate = DateTime.Now };

        cart.Deleted = true;
        cart.UpdatedAt = DateTime.Now;

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

}