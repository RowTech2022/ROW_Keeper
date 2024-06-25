using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1.Client1;

namespace Keeper.Core;

public class PlanEngine(ISqlFactory sql, DtoComplex dto)
{
    public Plan Create(Plan.Create create, UserInfoExtension userInfo)
    {
        var planId = new Db.Plan.Create
        {
            ReqUserId = userInfo.UserId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(planId);
    }

    public Plan Update(Plan.Update update)
    {
        var request = new Db.Plan.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Plan.Search.Result Search(Plan.Search filter)
    {
        var plans = new Db.Plan.Search().CopyFrom(filter, dto).Exec(sql);

        return new Plan.Search.Result
        {
            Items = plans.Select(x => new Plan.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = plans.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Plan Get(int id)
    {
        var plan = Check(id);

        return new Plan().CopyFrom(plan, dto);
    }

    public void Delete(Delete delete)
    {
        Check(delete.Id);

        var request = new Db.Plan.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdateList = [nameof(request.Active)];
        request.Exec(sql);
    }

    public Db.Plan.List.Result Check(int id)
    {
        var plan = new Db.Plan.List(id).Exec(sql).FirstOrDefault();

        if (plan == null)
            throw new RecordNotFoundApiException("Plan not found");

        return plan;
    }
}