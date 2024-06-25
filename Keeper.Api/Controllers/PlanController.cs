using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController(PlanEngine engine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Access.SystemAdmin)]
    public Plan Create(Plan.Create create)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        
        return engine.Create(create, userInfo);
    }
    
    [HttpPost("update")]
    [Authorize(Access.SystemAdmin)]
    public Plan Update(Plan.Update create)
    {
        return engine.Update(create);
    }

    [HttpPost("search")]
    public Plan.Search.Result Search(Plan.Search filter)
    {
        return engine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Plan Get(int id)
    {
        return engine.Get(id);
    }

    [HttpPost("delete")]
    [Authorize(Access.SystemAdmin)]
    public void Delete(Delete delete)
    {
        engine.Delete(delete);
    }
}