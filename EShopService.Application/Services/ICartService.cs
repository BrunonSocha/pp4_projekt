namespace EShopService.Application.Services;

public interface ICartService
{
    Task<Cart?> GetCartAsync(int cartId);
    Task<Cart> AddItemAsync(int cartId, int productId, int amount);
    Task<bool> RemoveItemAsync(int cartId, int productId);
    Task<bool> DeleteCartAsync(int cartId);
}