using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class OrganizationBranchEngine(ISqlFactory sql, DtoComplex dto)
{
    public OrganizationBranch Create(OrganizationBranch.Create create, UserInfo userInfo)
    {
        var resultId = new Db.OrganizationBranch.Create
        {
            ReqUserId = userInfo.UserId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public OrganizationBranch Update(OrganizationBranch.Update update)
    {
        Get(update.Id);
        
        var request = new Db.OrganizationBranch.Update().CopyFrom(update, dto);
        request.SetDefaultUpdationList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public OrganizationBranch.Search.Result Search(OrganizationBranch.Search filter)
    {
        var request = new Db.OrganizationBranch.Search().CopyFrom(filter, dto).Exec(sql);

        return new OrganizationBranch.Search.Result
        {
            Items = request.Select(x => new OrganizationBranch.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = request.Select(x => x.Id).FirstOrDefault()
        };
    }

    public OrganizationBranch Get(int id)
    {
        var request = new Db.OrganizationBranch.List(id).Exec(sql).FirstOrDefault();

        if (request == null)
            throw new RecordNotFoundApiException($"Organization branch with id {id} not found.");

        return new OrganizationBranch().CopyFrom(request, dto);
    }

    public void Delete(Delete delete)
    {
        Get(delete.Id);

        var request = new Db.OrganizationBranch.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdationList = [nameof(request.Active)];
        request.Exec(sql);
    }
}