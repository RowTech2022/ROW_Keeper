namespace Keeper.Core.Context.Models;

public class ProductPurchase : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }
    
    public int BranchId { get; set; }

    public int SupplierId { get; set; }

    public int PaymentTypeId { get; set; }

    public int BankAccountId { get; set; }

    public decimal TotalPrice { get; set; }

    public string AccountNumber { get; set; } = null!;
}