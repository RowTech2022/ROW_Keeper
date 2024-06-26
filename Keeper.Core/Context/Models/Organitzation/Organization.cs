using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Core.Context.Models;

[Index(nameof(OwnerId))]
public class Organization : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int OwnerId { get; set; }
    
    [StringLength(500)]
    public string OrgName { get; set; } = null!;

    [StringLength(5000)] 
    public string? OrgDescription { get; set; }

    [StringLength(20)]
    public string OrgPhone { get; set; } = null!;

    [StringLength(100)]
    public string? OrgEmail { get; set; }

    [StringLength(500)]
    public string OrgAddress { get; set; } = null!;

    [StringLength(100)]
    public string OwnerFullName { get; set; } = null!;

    [StringLength(100)]
    public string? OwnerEmail { get; set; } = null!;

    [StringLength(20)] 
    public string OwnerPhone { get; set; } = null!;

    public Client.Organization.OrgStatus Status { get; set; }
}