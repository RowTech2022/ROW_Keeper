using Keeper.Client;
using Keeper.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(CategoryEngine categoryEngine, RequestInfo requestInfo) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Access.Admin)]
    public Category Create(Category.Create create)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        return categoryEngine.Create(create, userInfo);
    }
    
    [HttpPost("update")]
    [Authorize(Access.Admin)]
    public Category Update(Category.Update update)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);
        return categoryEngine.Update(update);
    }

    [HttpPost("search")]
    [Authorize(Access.User)]
    public Category.Search.Result Search(Category.Search filter)
    {
        return categoryEngine.Search(filter);
    }

    [HttpGet("get/{id}")]
    [Authorize(Access.User)]
    public Category Get(int id)
    {
        return categoryEngine.Get(id);
    }

    [HttpPost("delete")]
    [Authorize(Access.Admin)]
    public void Delete(int id)
    {
        var userInfo = requestInfo.GetUserInfo(HttpContext);

        categoryEngine.Delete(id, userInfo);
    }
}