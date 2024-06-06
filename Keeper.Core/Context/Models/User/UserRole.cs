using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

[Keyless]
public class UserRole
{
    public int RoleId { get; set; }

    [StringLength(100)] 
    public string RoleName { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}