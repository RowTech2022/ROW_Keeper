using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController(OrganizationEngine organizationEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Access.AnyUser)]
    public Organization Create(Organization.Create create)
    {
        var userInfo = new UserInfo();// requestInfo.GetUserInfo(HttpContext);
        return organizationEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    [Authorize(Access.Admin)]
    public Organization Update(Organization.Update update)
    {
        return organizationEngine.Update(update);
    }

    [HttpPost("search")]
    public Organization.Search.Result Search(Organization.Search filter)
    {
        return organizationEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Organization Get(int id)
    {
        return organizationEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        organizationEngine.Delete(delete);
    }
}