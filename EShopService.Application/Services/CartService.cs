//using EShopService;
//namespace EShopService.Application.Services;
//using EShopAbstractions;

//public class CartService : ICartService
//{
//    private readonly EShopDbContext _dbContext;

//    public CartService(EShopDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<Cart?> GetCartAsync(int cartId)
//    {
//        var cart = await _dbContext.Carts
//            .Include(c => c.Items)
//            .ThenInclude(i => i.Product)
//            .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);
//    }

//    public async Task<Cart> AddItemAsync(int cartId, int productId, int amount)
//    {
//        var cart = await _dbContext.Carts
//            .Include(c => c.Items)
//            .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

//        if (cart == null)
//        {
//            cart = new Cart { };
//            _dbContext.Carts.Add(cart);
//            await _dbContext.SaveChangesAsync();
//        }

//        var product = await _dbContext.Products
//            .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);

//        if (product == null)
//            throw new Exception("Product doesn't exist.");

//        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

//        if (existingItem != null)
//            existingItem.Amount += amount;
//        else
//            cart.Items.Add(new CartProduct { CartId = cart.Id, ProductId = productId, Amount = amount });

//        cart.UpdatedAt = DateTime.Now;
//        await _dbContext.SaveChangesAsync();

//        return cart;
//    }

//    public async Task<bool> RemoveItemAsync(int cartId, int productId)
//    {
//        var cart = await _dbContext.Carts
//            .Include(c => c.Items)
//            .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);
//        if (cart == null)
//            return false;

//        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
//        if (item == null)
//            return false;

//        cart.Items.Remove(item);
//        cart.UpdatedAt = DateTime.Now;

//        await _dbContext.SaveChangesAsync();

//        return true;
//    }


//    public async Task<bool> DeleteCartAsync(int cartId)
//    {
//        var cart = await _dbContext.Carts
//            .FirstOrDefaultAsync(c => c.Id == cartId && !c.Deleted);

//        if (cart == null)
//            return false;

//        cart.Deleted = true;
//        cart.UpdatedAt = DateTime.Now;
//        await _dbContext.SaveChangesAsync;
//        return true;
//    }
//}