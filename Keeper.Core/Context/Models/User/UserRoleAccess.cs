using Microsoft.EntityFrameworkCore;

namespace Keeper.Core.Context.Models;

[Keyless]
public class UserRoleAccess
{
    public int UserId { get; set; }
    public int RegUserId { get; set; }
    public int RoleId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}