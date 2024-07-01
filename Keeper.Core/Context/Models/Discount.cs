using System.ComponentModel.DataAnnotations;
using Keeper.Client;

namespace Keeper.Core.Context.Models;

public class Discount : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }
    
    public int? ProductId { get; set; }

    public int? CategoryId { get; set; }
    
    public double Percent { get; set; }
    
    [StringLength(5000)]
    public string? Comment { get; set; }
    
    public DateTimeOffset FromDate { get; set; }
    
    public DateTimeOffset ToDate { get; set; }

    public DiscountType Type { get; set; }
}