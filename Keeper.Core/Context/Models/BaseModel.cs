using System.ComponentModel.DataAnnotations;

namespace Keeper.Core.Context.Models;

public class BaseModel
{
    public bool Active { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    [Timestamp]
    public byte[]? Timestamp { get; set; }
}