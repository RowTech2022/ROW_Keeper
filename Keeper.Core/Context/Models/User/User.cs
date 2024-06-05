using System.ComponentModel.DataAnnotations;
using Keeper.Client;

namespace Keeper.Core.Context.Models;

public class User : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int BranchId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Surname { get; set; } = null!;

    public UserType Usertype { get; set; }

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)] 
    public string Login { get; set; } = null!;
        
    public byte[]? PasswordHash { get; set; }
}