using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class Organization : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int OwnerId { get; set; }
    
    [StringLength(500)]
    public string OrgName { get; set; } = null!;

    [StringLength(20)]
    public string OrgPhone { get; set; } = null!;

    [StringLength(100)]
    public string? OrgEmail { get; set; }

    [StringLength(500)]
    public string OrgAddress { get; set; } = null!;
}