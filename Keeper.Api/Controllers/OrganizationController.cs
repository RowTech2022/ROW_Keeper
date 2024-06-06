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
}