using Bibliotekaen.Sql.Data;

namespace Keeper.Core;

[BindStruct]
public class BaseProp
{
    [Bind("Active")]
    public bool Active { get; set; }
    
    [Bind("CreatedAt")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Bind("UpdatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }

    [Bind("Timestamp")]
    public byte[] Timestamp { get; set; } = null!;
}