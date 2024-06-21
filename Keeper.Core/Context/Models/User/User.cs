using System.ComponentModel.DataAnnotations;
using Keeper.Client;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Core.Context.Models;

[Index(nameof(Login), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Phone), IsUnique = true)]
public class User : BaseModel
{
    public int Id { get; set; }

    public int ReqUserId { get; set; }

    public int BranchId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public UserType Usertype { get; set; }

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)] 
    public string Login { get; set; } = null!;

    public Client.User.Status Status { get; set; }
        
    public byte[]? PasswordHash { get; set; }
}