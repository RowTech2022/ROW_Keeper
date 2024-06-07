using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationBranchController(OrganizationBranchEngine orgBranchEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    public OrganizationBranch Craete(OrganizationBranch.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        return orgBranchEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    public OrganizationBranch Update(OrganizationBranch.Update update)
    {
        return orgBranchEngine.Update(update);
    }

    [HttpPost("search")]
    public OrganizationBranch.Search.Result Search(OrganizationBranch.Search filter)
    {
        return orgBranchEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public OrganizationBranch Get(int id)
    {
        return orgBranchEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        orgBranchEngine.Delete(delete);
    }
}