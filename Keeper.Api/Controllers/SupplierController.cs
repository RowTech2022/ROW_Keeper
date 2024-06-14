using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController(SupplierEngine supplierEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    public Supplier Create(Supplier.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        userInfo.OrganisationId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "OrganizationId")!.Value);
        return supplierEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    public Supplier Update(Supplier.Update update)
    {
        return supplierEngine.Update(update);
    }

    [HttpPost("search")]
    public Supplier.Search.Result Search(Supplier.Search filter)
    {
        return supplierEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Supplier Get(int id)
    {
        return supplierEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        supplierEngine.Delete(delete);
    }
}