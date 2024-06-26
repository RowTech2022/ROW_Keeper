using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Keeper.Client.ProductDiscount;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class ProductDiscountEngine(ISqlFactory sql, DtoComplex dto)
{
    public Discount Create(Discount.Create create, UserInfo userInfo)
    {
        var product = new Db.Product.List(create.ProductId ?? 0).Exec(sql).FirstOrDefault();

        if (product == null)
        {
            throw new RecordNotFoundApiException($"Product with id {create.ProductId} not found");
        }
        
        var resultId = new Db.Discount.Create { ReqUserId = userInfo.UserId }.CopyFrom(create, dto).Ecec(sql);

        return Get(resultId);
    }

    public Discount Update(Discount.Update update)
    {
        Check(update.Id);

        var request = new Db.Discount.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Discount.Search.Result Search(Discount.Search filter)
    {
        var request = new Db.Discount.Search().CopyFrom(filter, dto).Exec(sql);

        return new Discount.Search.Result
        {
            Items = request.Select(x => new Discount.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = request.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Discount Get(int id)
    {
        var request = Check(id);

        return new Discount().CopyFrom(request, dto);
    }

    public void Delete(Delete delete)
    {
        Check(delete.Id);

        var request = new Db.Discount.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdateList = [nameof(request.Active)];
        request.Exec(sql);
    }

    public Db.Discount.List.Result Check(int id)
    {
        var request = new Db.Discount.List(id).Ecec(sql).FirstOrDefault();

        if (request == null)
            throw new RecordNotFoundApiException($"Product discount with id {id} not found.");

        return request;
    }
}