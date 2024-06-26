using Keeper.Client;
using Keeper.Client.ProductDiscount;
using Keeper.Core;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDiscountController(ProductDiscountEngine productDiscountEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    public Discount Create(Discount.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        
        return productDiscountEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    public Discount Update(Discount.Update update)
    {
        return productDiscountEngine.Update(update);
    }

    [HttpPost("search")]
    public Discount.Search.Result Search(Discount.Search filter)
    {
        return productDiscountEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public Discount Get(int id)
    {
        return productDiscountEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        productDiscountEngine.Delete(delete);
    }
}