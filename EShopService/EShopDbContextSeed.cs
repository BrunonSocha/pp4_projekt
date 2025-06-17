namespace EShopService
{
    public class EShopDbContextSeed(EShopDbContext context) : IEShopDbContextSeed
    {
        public void Seed()
        {
            if (!context.Categories.Any())
            {
                var fruits = new Category
                {
                    Name = "Fruits",
                    CreatedBy = Guid.NewGuid()
                };

                var vegetables = new Category
                {
                    Name = "Vegetables",
                    CreatedBy = Guid.NewGuid()
                };

                context.Categories.AddRange(fruits, vegetables);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var vegetables = context.Categories.FirstOrDefault(c => c.Name == "vegetables");
                var fruits = context.Categories.FirstOrDefault(c => c.Name == "fruits");
                var products = new List<Product>
                {
                    new Product { Name = "Carrot", Category = vegetables, Price= 15, Stock = 100, Ean = "111111", Sku = "CR001", CreatedBy = Guid.NewGuid() },
                    new Product { Name = "Onion", Category = vegetables, Price= 15, Stock = 120, Ean = "222222", Sku = "ON002", CreatedBy = Guid.NewGuid() },
                    new Product { Name = "Cherry", Category = fruits, Price= 15, Stock = 80, Ean = "333333", Sku = "CH003", CreatedBy = Guid.NewGuid() },
                    new Product { Name = "Apple", Category = fruits, Price= 15, Stock = 150, Ean = "444444", Sku = "AP004", CreatedBy = Guid.NewGuid() },
                    new Product { Name = "Melon", Category = fruits, Price= 15, Stock = 50, Ean = "555555", Sku = "ME005", CreatedBy = Guid.NewGuid() }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
