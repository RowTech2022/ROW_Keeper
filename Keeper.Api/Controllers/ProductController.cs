using Keeper.Client;
using Keeper.Client.Product;
using Keeper.Core;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductEngine productEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    public Product Create(Product.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);

        return productEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    public Product Update(Product.Update update)
    {
        return productEngine.Update(update);
    }

    [HttpPost("search")]
    public Product.Search.Result Search(Product.Search filter)
    {
        return productEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Product Get(int id)
    {
        return productEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        productEngine.Delete(delete);
    }
}