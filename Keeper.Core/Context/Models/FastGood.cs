namespace Keeper.Core.Context.Models;

public class FastGood : BaseModel
{
    public int Id { get; set; }
    public int ReqUserId { get; set; }
    public int OrgId { get; set; }
    public int ProductId { get; set; }
}