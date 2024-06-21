using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class ProductDiscount : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }
    
    public int ProductId { get; set; }
    
    public double Percent { get; set; }
    
    [StringLength(5000)]
    public string? Comment { get; set; }
    
    public DateTimeOffset FromDate { get; set; }
    
    public DateTimeOffset ToDate { get; set; }
}