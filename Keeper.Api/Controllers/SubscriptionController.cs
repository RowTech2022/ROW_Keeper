using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController(SupscriptionEngine supscriptionEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Access.SystemAdmin)]
    public Subscription Create(Subscription.Create create)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);

        return supscriptionEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    [Authorize(Access.SystemAdmin)]
    public Subscription Update(Subscription.Update update)
    {
        return supscriptionEngine.Update(update);
    }

    [HttpPost("search")]
    public Subscription.Search.Result Search(Subscription.Search filter)
    {
        return supscriptionEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Subscription Get(int id)
    {
        return supscriptionEngine.Get(id);
    }

    [HttpPost("delete")]
    [Authorize(Access.SystemAdmin)]
    public void Delete(Delete delete)
    {
        supscriptionEngine.Delete(delete);
    }
}