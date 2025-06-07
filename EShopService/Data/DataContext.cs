using System.Collections.Generic;
using System.Reflection.Emit;
using EShopService.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Database connection string
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Product-Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
