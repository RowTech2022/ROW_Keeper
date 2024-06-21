namespace Keeper.Core.Context.Models;

public class ProductPurchaseDetail : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTimeOffset ExpireDate { get; set; }
}