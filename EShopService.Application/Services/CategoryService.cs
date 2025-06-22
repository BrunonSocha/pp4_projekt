using EShop.Application.Services;
using EShopAbstractions;
using EShopAbstractions.Models;
using EShopService;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly EShopDbContext _dbContext;

    public CategoryService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbContext.Categories
            .Where(c => !c.Deleted)
            .ToListAsync();
    }

    public async Task<Category?> GetOneAsync(int id)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
    }

    public async Task<Category> CreateAsync(Category category, Guid userId)
    {
        category.CreatedAt = DateTime.Now;
        category.UpdatedAt = DateTime.Now;
        category.CreatedBy = userId;
        category.UpdatedBy = userId;
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<bool> UpdateAsync(int id, Category updated, Guid userId)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        if (category == null)
            return false;

        category.Name = updated.Name;
        category.UpdatedAt = DateTime.Now;
        category.UpdatedBy = userId;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id, Guid userId)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null || category.Deleted)
            return false;

        category.Deleted = true;
        category.UpdatedAt = DateTime.Now;
        category.UpdatedBy = userId;

        await _dbContext.SaveChangesAsync();
        return true;
    }
}