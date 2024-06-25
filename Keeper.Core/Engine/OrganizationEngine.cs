using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class OrganizationEngine(PlanEngine planEngine, ISqlFactory sql, DtoComplex dto)
{
    public Organization Create(Organization.Create create, UserInfo userInfo)
    {
        if (create.OwnerId != 0)
        {
            var user = new Db.User.CheckUser(create.OwnerId).Exec(sql);
            if (user == null)
                throw new RecordNotFoundApiException($"User with id {create.OwnerId} not found.");
        }

        var plan = create.PlanId != 0
            ? planEngine.Check(create.PlanId)
            : null;

        var resultId = new Db.Organization.Create
        {
            ReqUserId = userInfo.UserId
        }.CopyFrom(create, dto).Exec(sql);

        if (plan != null)
        {
            new Db.Subscription.Create
            {
                ReqUserId = userInfo.UserId,
                OrgId = resultId,
                PlanId = plan.Id,
                StartDate = DateTimeOffset.Now,
                EndDate = plan.Type switch
                {
                    Plan.PlanType.Monthly => DateTimeOffset.Now.AddDays(plan.Duration),
                    Plan.PlanType.Annual => DateTimeOffset.Now.AddYears(plan.Duration),
                    Plan.PlanType.Trial => DateTimeOffset.Now.AddDays(plan.Duration),
                    Plan.PlanType.Free => DateTimeOffset.Now.AddYears(999),
                    _ => DateTimeOffset.Now
                }
            }.Exec(sql);
        }

        return Get(resultId);
    }

    public Organization Update(Organization.Update update)
    {
        Get(update.Id);

        var request = new Db.Organization.Update().CopyFrom(update, dto);
        request.SetDefaultUpdationList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Organization.Search.Result Search(Organization.Search filter)
    {
        var organizations = new Db.Organization.Search().CopyFrom(filter, dto).Exec(sql);

        return new Organization.Search.Result
        {
            Items = organizations.Select(x => new Organization.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = organizations.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Organization Get(int id)
    {
        var organization = new Db.Organization.List(id).Exec(sql).FirstOrDefault();
        if (organization == null)
            throw new RecordNotFoundApiException($"Organization with id {id} not found.");

        return new Organization().CopyFrom(organization, dto);
    }

    public void Delete(Delete delete)
    {
        Get(delete.Id);

        var request = new Db.Organization.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdationList = [nameof(request.Active)];
        request.Exec(sql);
    }
}