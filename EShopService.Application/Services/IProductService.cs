using EShopAbstractions.Models;

namespace EShopService.Application.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetOneAsync(int id);
    Task<Product> CreateAsync(Product product, Guid userId);
    Task<bool> UpdateAsync(int id, Product updated, Guid userId);
    Task<bool> DeleteAsync(int id, Guid userId);
}