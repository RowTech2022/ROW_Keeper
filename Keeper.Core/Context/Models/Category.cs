using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class Category : BaseModel
{
    public int Id { get; set; }
    
    public int ReqUserId { get; set; }

    public int OrgId { get; set; }
    
    public int? ParentId { get; set; }
    
    [StringLength(300)]
    public string Name { get; set; } = null!;
}