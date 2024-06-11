using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Keeper.Client.Product;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class ProductEngine(ISqlFactory sql, DtoComplex dto)
{
    public Product Create(Product.Create create, UserInfo userInfo)
    {
        var resultId = new Db.Product.Create
        {
            ReqUserId = userInfo.UserId,
            BranchId = userInfo.OrganisationId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public Product Update(Product.Update update)
    {
        var request = new Db.Product.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Product.Search.Result Search(Product.Search filter)
    {
        var products = new Db.Product.Search().CopyFrom(filter, dto).Exec(sql);

        return new Product.Search.Result
        {
            Items = products.Select(x => new Product.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = products.Select(x => x.Total).FirstOrDefault()
        };
    }

    public Product Get(int id)
    {
        var product = Check(id);

        return new Product().CopyFrom(product, dto);
    }

    public void Delete(Delete delete)
    {
        Check(delete.Id);

        var request = new Db.Product.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdateList = [nameof(request.Active)];
        request.Exec(sql);
    }

    private Db.Product.List.Result Check(int id)
    {
        var product = new Db.Product.List(id).Exec(sql).FirstOrDefault();

        if (product == null)
            throw new RecordNotFoundApiException($"Product with id {id} not found");

        return product;
    }
}