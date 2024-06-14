using System.Net;
using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class CategoryEngine(ISqlFactory sql, DtoComplex dto)
{
    public Category Create(Category.Create create, UserInfo userInfo)
    {
        var org = new Db.Organization.List(userInfo.OrganisationId).Exec(sql).FirstOrDefault();
        if (org == null)
            throw new RecordNotFoundApiException($"Organization with id {userInfo.OrganisationId} not found.");

        if (create.ParentId != null)
        {
            Check((int)create.ParentId);
        }
        
        var categoryId = new Db.Category.Create
        {
            ReqUserId = userInfo.UserId,
            OrgId = userInfo.OrganisationId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(categoryId);
    }

    public Category Update(Category.Update update)
    {
        Get(update.Id);

        var request = new Db.Category.Update().CopyFrom(update, dto);
        request.SetUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Category.Search.CategoryResult SearchCategory(Category.Search filter)
    {
        var request = new Db.Category.Search().CopyFrom(filter, dto).Exec(sql);

        return new Category.Search.CategoryResult
        {
            Items = request.Select(x => new Category.Search.CategoryResult.Item().CopyFrom(x, dto)).ToList(),
            Total = request.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Category.Search.SubCategoryResult SearchSubCategory(Category.Search filter)
    {
        var request = new Db.Category.SearchSubCategory().CopyFrom(filter, dto).Exec(sql);

        return new Category.Search.SubCategoryResult
        {
            Items = request.Select(x => new Category.Search.SubCategoryResult.Item().CopyFrom(x, dto)).ToList(),
            Total = request.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Category Get(int id)
    {
        var category = Check(id);

        return new Category().CopyFrom(category, dto);
    }

    public void Delete(int id, UserInfo userInfo)
    {
        var category = Check(id);

        if (category.OrgId != userInfo.OrganisationId)
            throw new ApiException("You do not have access to delete this category.", HttpStatusCode.Forbidden);

        var request = new Db.Category.Update { Active = false };
        request.UpdateList = [ nameof(request.Active) ];
        request.Exec(sql);
    }

    private Db.Category.List.Result Check(int id)
    {
        var category = new Db.Category.List(id).Exec(sql).FirstOrDefault();

        if (category == null)
            throw new RecordNotFoundApiException($"Category with id {id} not found.");

        return category;
    }
}