using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class Supplier : BaseModel
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    [StringLength(20)]
    public string Phone { get; set; } = null!;
    
    [StringLength(100)] 
    public string? Email { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }
}