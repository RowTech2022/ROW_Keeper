namespace Keeper.Core.Context.Models;

public class Subscription : BaseModel
{
    public int Id { get; set; }
    public int ReqUserId { get; set; }
    public int OrgId { get; set; }
    public int PlanId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}