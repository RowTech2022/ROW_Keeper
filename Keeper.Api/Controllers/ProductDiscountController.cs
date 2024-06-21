using Bibliotekaen.Dto;
using Keeper.Client;
using Keeper.Client.ProductDiscount;
using Keeper.Core;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductDiscountController(ProductDiscountEngine productDiscountEngine, DtoComplex dto, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    public ProductDiscount Create(ProductDiscount.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        
        return productDiscountEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    public ProductDiscount Update(ProductDiscount.Update update)
    {
        return productDiscountEngine.Update(update);
    }

    [HttpPost("search")]
    public ProductDiscount.Search.Result Search(ProductDiscount.Search filter)
    {
        return productDiscountEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    public ProductDiscount Get(int id)
    {
        return productDiscountEngine.Get(id);
    }

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        productDiscountEngine.Delete(delete);
    }
}