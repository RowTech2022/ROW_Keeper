using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class ProductDitscount : BaseProp
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public decimal Percent { get; set; }
    
    [StringLength(5000)]
    public string Comment { get; set; } = null!;
    
    public DateTimeOffset FromDate { get; set; }
    
    public DateTimeOffset ToDate { get; set; }
}