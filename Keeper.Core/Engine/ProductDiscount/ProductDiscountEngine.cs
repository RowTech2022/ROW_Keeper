using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Keeper.Client.ProductDiscount;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class ProductDiscountEngine(ISqlFactory sql, DtoComplex dto)
{
    public ProductDiscount Create(ProductDiscount.Create create, UserInfo userInfo)
    {
        var product = new Db.Product.List(create.ProductId).Exec(sql).FirstOrDefault();

        if (product == null)
        {
            throw new RecordNotFoundApiException($"Product with id {create.ProductId} not found");
        }
        
        var resultId = new Db.ProductDiscount.Create { ReqUserId = userInfo.UserId }.CopyFrom(create, dto).Ecec(sql);

        return Get(resultId);
    }

    public ProductDiscount Update(ProductDiscount.Update update)
    {
        Check(update.Id);

        var request = new Db.ProductDiscount.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public ProductDiscount.Search.Result Search(ProductDiscount.Search filter)
    {
        var request = new Db.ProductDiscount.Search().CopyFrom(filter, dto).Exec(sql);

        return new ProductDiscount.Search.Result
        {
            Items = request.Select(x => new ProductDiscount.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = request.Select(x => x.Total).FirstOrDefault()
        };
    }

    public ProductDiscount Get(int id)
    {
        var request = Check(id);

        return new ProductDiscount().CopyFrom(request, dto);
    }

    public void Delete(Delete delete)
    {
        Check(delete.Id);

        var request = new Db.ProductDiscount.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdateList = [nameof(request.Active)];
        request.Exec(sql);
    }

    public Db.ProductDiscount.List.Result Check(int id)
    {
        var request = new Db.ProductDiscount.List(id).Ecec(sql).FirstOrDefault();

        if (request == null)
            throw new RecordNotFoundApiException($"Product discount with id {id} not found.");

        return request;
    }
}