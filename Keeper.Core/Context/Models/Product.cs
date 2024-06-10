using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Core.Context.Models;

[Index(nameof(BranchId))]
[Index(nameof(UPC), IsUnique = true)]
public class Product : BaseModel
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public int SupplierId { get; set; }

    public int CategoryId { get; set; }

    public int TaxId { get; set; }
    
    [StringLength(100)]
    public string UPC { get; set; } = null!;
    
    [StringLength(500)]
    public string Name { get; set; } = null!;

    public int AgeLimit { get; set; }

    public int Quantity { get; set; }

    public decimal BuyingPrice { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPrice { get; set; }

    public decimal TotalPrice { get; set; }
    
    public int Margin { get; set; }

    public bool HaveDiscount { get; set; }
    
    public DateTimeOffset? ExpiredDate { get; set; }
}