using System.Runtime.CompilerServices;
using EShopAbstractions.Models;
using EShopService;
using Microsoft.EntityFrameworkCore;

namespace EShopAbstractions
{
        public class EShopDbContext : DbContext
        {
        public EShopDbContext()
        {
        }
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Cart> Carts { get; set; } = default!;

        public DbSet<CartProduct> CartProducts { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EShopDb;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(255);
            modelBuilder.Entity<Product>()
                .Property(pr => pr.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(255);

            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(cp => cp.CartId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany()
                .HasForeignKey(cp => cp.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cart)
                .WithOne()
                .HasForeignKey<Order>(o => o.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255);

        }
    }
}
