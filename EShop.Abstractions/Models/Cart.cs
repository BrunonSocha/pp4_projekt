using EShopAbstractions;
using EShopService;

namespace EShopAbstractions.Models;

public class Cart : BaseModel
{ 
    public Guid UserId { get; set; }

    public List<CartProduct> Items { get; set; } = new();

}