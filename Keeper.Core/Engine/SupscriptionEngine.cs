using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class SupscriptionEngine(ISqlFactory sql, DtoComplex dto)
{
    public Subscription Create(Subscription.Create create, UserInfo userInfo)
    {
        var resultId = new Db.Subscription.Create
        {
            ReqUserId = userInfo.UserId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public Subscription Update(Subscription.Update update)
    {
        Check(update.Id);

        var request = new Db.Subscription.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Subscription.Search.Result Search(Subscription.Search filter)
    {
        var subs = new Db.Subscription.Search().CopyFrom(filter, dto).Exec(sql);

        return new Subscription.Search.Result
        {
            Items = subs.Select(x => new Subscription.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = subs.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Subscription Get(int id)
    {
        var subs = Check(id);

        return new Subscription().CopyFrom(subs, dto);
    }

    public void Delete(Delete delete)
    {
        var subs = Check(delete.Id);

        var request = new Db.Subscription.Update
        {
            Id = subs.Id,
            EndDate = DateTimeOffset.Now,
            Active = false
        };
        request.UpdateList =
        [
            nameof(request.EndDate),
            nameof(request.Active),
        ];
        request.Exec(sql);
    }

    private Db.Subscription.List.Result Check(int id)
    {
        var subs = new Db.Subscription.List(id).Exec(sql).FirstOrDefault()
                   ?? throw new RecordNotFoundApiException("Subscription not found");

        return subs;
    }
}