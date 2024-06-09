namespace Keeper.Core.Context.Models;

public class Category : BaseModel
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = null!;
}