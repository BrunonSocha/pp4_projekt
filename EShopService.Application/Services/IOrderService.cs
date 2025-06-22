using EShopAbstractions;
namespace EShopService.Application.Services;

public interface IOrderService
{
    Task<Order?> GetOrderAsync(int orderId);
    Task<Order> CreateOrderAsync(Guid userid, int cartId);
}