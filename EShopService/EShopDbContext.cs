using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace EShopService
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

            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(255);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(255);

            modelBuilder.Entity<Cart>()
                .HasKey(c => c.UserId);

            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.CartUserId, cp.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(cp => cp.CartUserId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany()
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}
