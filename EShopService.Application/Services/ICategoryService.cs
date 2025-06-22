namespace EShop.Application.Services;

using EShopAbstractions.Models;
using EShopService;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetOneAsync(int id);
    Task<Category> CreateAsync(Category category, Guid userId);
    Task<bool> UpdateAsync(int id, Category updated, Guid userId);
    Task<bool> DeleteAsync(int id, Guid userId);

}