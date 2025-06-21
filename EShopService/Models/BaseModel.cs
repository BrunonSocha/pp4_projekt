namespace EShopService;

public abstract class BaseModel
{
    public int Id { get; set; }

    public bool Deleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public Guid? UpdatedBy { get; set; }
}