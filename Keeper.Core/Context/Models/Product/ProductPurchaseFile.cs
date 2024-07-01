namespace Keeper.Core.Context.Models;

public class ProductPurchaseFile : BaseModel
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public string Url { get; set; } = null!;
}