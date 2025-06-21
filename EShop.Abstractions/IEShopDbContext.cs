using EShopAbstractions.Models;
using EShopService;
using Microsoft.EntityFrameworkCore;

namespace EShopAbstractions
{
    public interface IEShopDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Category> Categories { get; }
        DbSet<Cart> Carts { get; }
        DbSet<CartProduct> CartProducts { get; }
        DbSet<User> Users { get; }
        DbSet<Order> Orders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
