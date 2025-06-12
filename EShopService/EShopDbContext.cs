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
                    .HasForeignKey("CategoryId")
                    .IsRequired();

                modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(255);
                modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(255);
            }
        }
    }
