using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class OrganizationEngine(ISqlFactory sql, DtoComplex dto)
{
    public Organization Create(Organization.Create create, UserInfo userInfo)
    {
        var user = new Db.User.CheckUser(create.OwnerId).Exec(sql);
        if (user == null)
            throw new RecordNotFoundApiException($"User with id {create.OwnerId} not found.");

        var resultId = new Db.Organization.Create
        {
            ReqUserId = userInfo.UserId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public Organization Get(int id)
    {
        var organization = new Db.Organization.List(id).Exec(sql).FirstOrDefault();
        if (organization == null)
            throw new RecordNotFoundApiException($"Organization with id {id} not found.");

        return new Organization().CopyFrom(organization, dto);
    }
}