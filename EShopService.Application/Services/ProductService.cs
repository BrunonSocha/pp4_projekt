using EShopAbstractions;
using EShopAbstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Application.Services;

public class ProductService : IProductService
{
    private readonly EShopDbContext _dbContext;

    public ProductService(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products
            .Where(p => !p.Deleted)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetOneAsync(int id)
    {
        return await _dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
    }

    public async Task<Product> CreateAsync(Product product, Guid userId)
    {
        if (!await _dbContext.Categories.AnyAsync(c => c.Id == product.CategoryId && !c.Deleted))
            throw new Exception("Category doesn't exist.");

        product.CreatedAt = DateTime.Now;
        product.CreatedBy = userId;
        product.UpdatedAt = DateTime.Now;
        product.CreatedBy = userId;

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;

    }

    public async Task<bool> UpdateAsync(int id, Product updated, Guid userId)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product == null || product.Deleted)
            return false;

        if (!await _dbContext.Categories.AnyAsync(c => c.Id == updated.CategoryId && !c.Deleted))
            throw new Exception("Category doesn't exist.");

        product.Name = updated.Name;
        product.Ean = updated.Ean;
        product.Sku = updated.Sku;
        product.Stock = updated.Stock;
        product.Price = updated.Price;
        product.CategoryId = updated.CategoryId;
        product.UpdatedAt = DateTime.Now;
        product.UpdatedBy = userId;
        await _dbContext.SaveChangesAsync();
        return true;

    }

    public async Task<bool> DeleteAsync(int id, Guid userId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.Deleted);
        if (product == null)
            return false;
        product.Deleted = true;
        product.UpdatedAt = DateTime.Now;
        product.UpdatedBy = userId;
        await _dbContext.SaveChangesAsync();
        return true;
    }

}