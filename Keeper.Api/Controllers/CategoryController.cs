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
        var userInfo = requestInfo.GetUserInfoHelper(HttpContext);
        return categoryEngine.Create(create, userInfo);
    }
    
    [HttpPost("update")]
    [Authorize(Access.Admin)]
    public Category Update(Category.Update update)
    {
        return categoryEngine.Update(update);
    }

    [HttpPost("searchCategory")]
    [Authorize(Access.User)]
    public Category.Search.CategoryResult SearchCategory(Category.Search filter)
    {
        return categoryEngine.SearchCategory(filter);
    }

    [HttpPost("searchSubCategory")]
    [Authorize(Access.User)]
    public Category.Search.SubCategoryResult SearchSubCategory(Category.Search filter)
    {
        return categoryEngine.SearchSubCategory(filter);
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