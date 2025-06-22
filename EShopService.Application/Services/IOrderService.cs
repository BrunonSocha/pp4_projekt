using EShopAbstractions;
namespace EShopService.Application.Services;

public interface IOrderService
{
    Task<object?> GetOrderAsync(int orderId);
    Task<Order> CreateOrderAsync(Guid userid, int cartId);
}