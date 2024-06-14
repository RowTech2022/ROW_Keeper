using System.Net;
using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Keeper.Client.Product;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public partial class ProductEngine(ISqlFactory sql, DtoComplex dto)
{
    public Product Create(Product.Create create, UserInfo userInfo)
    {
        var supplier = new Db.Supplier.List(create.SupplierId).Exec(sql).FirstOrDefault();

        if (supplier == null)
            throw new RecordNotFoundApiException($"Supplier with id {create.SupplierId} not found.");

        var category = new Db.Category.List(create.CategoryId).Exec(sql).FirstOrDefault();

        if (category == null)
            throw new RecordNotFoundApiException($"Category with id {create.CategoryId} not found.");
        
        CheckUPC(create.UPC, userInfo);
        
        var resultId = new Db.Product.Create
        {
            ReqUserId = userInfo.UserId,
            BranchId = userInfo.OrganisationId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public Product Update(Product.Update update, UserInfo userInfo)
    {
        var product = Check(update.Id);
        
        var category = new Db.Category.List(update.CategoryId).Exec(sql).FirstOrDefault();

        if (category == null)
            throw new RecordNotFoundApiException($"Category with id {update.CategoryId} not found.");
        
        if (product.UPC != update.UPC)
            CheckUPC(update.UPC, userInfo);
        
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

    private void CheckUPC(string upc, UserInfo userInfo)
    {
        var product = new Db.Product.CheckUPC
        {
            UPC = upc,
            BranchId = userInfo.OrganisationId
        }.Exec(sql).FirstOrDefault();

        if (product != null)
            throw new ApiException($"Product with UPC {upc} already exist.", HttpStatusCode.Conflict);
    }
}