using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class ProductPurchase : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }
    
    public int OrgId { get; set; }

    public int SupplierId { get; set; }

    public int PaymentTypeId { get; set; }

    public int BankAccount { get; set; }

    public decimal TotalPrice { get; set; }

    [StringLength(100)]
    public string AccountNumber { get; set; } = null!;
}