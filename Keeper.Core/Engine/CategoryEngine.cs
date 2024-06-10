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

        if (create.ParentId != 0)
        {
            Check(create.ParentId);
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

    public Category.Search.Result Search(Category.Search filter)
    {
        var request = new Db.Category.Search().CopyFrom(filter, dto).Exec(sql);

        return new Category.Search.Result
        {
            Items = request.Select(x => new Category.Search.Result.Item
            {
                SubCategories = request.Where(s => s.Id == x.Id)
                    .Select(s => new Category.Search.Result.Item().CopyFrom(s, dto)).ToList()
            }.CopyFrom(x, dto)).ToList(),
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