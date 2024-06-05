using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class OrganizationBranch : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int OwnerId { get; set; }
    
    [StringLength(500)]
    public string BranchName { get; set; } = null!;

    [StringLength(20)] 
    public string BranchPhone { get; set; } = null!;

    [StringLength(500)]
    public string BranchAddress { get; set; } = null!;
}