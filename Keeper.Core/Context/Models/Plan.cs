using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class Plan : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    [StringLength(500)]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Duration { get; set; }
    
    public Client.Plan.PlanType Type { get; set; }
}