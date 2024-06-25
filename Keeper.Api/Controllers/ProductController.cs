using Keeper.Client;
using Keeper.Client.Product;
using Keeper.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductEngine productEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Access.User)]
    public Product Create(Product.Create create)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        
        return productEngine.Create(create, userInfo);
    }

    [HttpPost("update")]
    [Authorize(Access.User)]
    public Product Update(Product.Update update)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        
        return productEngine.Update(update, userInfo);
    }

    [HttpPost("import")]
    [Authorize(Access.User)]
    public async Task<List<Product.Update>> Import(IFormFile file)
    {
        return await productEngine.ImportProduct(file);
    }

    [HttpPost("search")]
    public Product.Search.Result Search(Product.Search filter)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        
        return productEngine.Search(filter, userInfo);
    }

    [HttpGet("get/{id}")]
    public Product Get(int id)
    {
        return productEngine.Get(id);
    }

    [HttpPost("checkUPC")]
    [Authorize(Access.User)]
    public bool CheckUPC(Product.CheckUPC upc)
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);

        return productEngine.CheckUPC(upc.UPC, userInfo);
    }

    [HttpGet("generateUPC")]
    [Authorize(Access.User)]
    public Product.CheckUPC GenerateUPC()
    {
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        
        return productEngine.GetnerateUPC(userInfo);
    }
    

    [HttpPost("delete")]
    public void Delete(Delete delete)
    {
        productEngine.Delete(delete);
    }
}