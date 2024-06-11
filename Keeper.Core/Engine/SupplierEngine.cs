using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Keeper.Client;
using Row.Common1;
using Row.Common1.Client1;

namespace Keeper.Core;

public class SupplierEngine(ISqlFactory sql, DtoComplex dto)
{
    public Supplier Create(Supplier.Create create, UserInfo userInfo)
    {
        var resultId = new Db.Supplier.Create
        {
            ReqUserId = userInfo.UserId,
            OrgId = userInfo.OrganisationId
        }.CopyFrom(create, dto).Exec(sql);

        return Get(resultId);
    }

    public Supplier Update(Supplier.Update update)
    {
        Check(update.Id);
        
        var request = new Db.Supplier.Update().CopyFrom(update, dto);
        request.SetDefaultUpdateList();
        request.Exec(sql);

        return Get(update.Id);
    }

    public Supplier.Search.Result Search(Supplier.Search filter)
    {
        var suppliers = new Db.Supplier.Search().CopyFrom(filter, dto).Exec(sql);

        return new Supplier.Search.Result
        {
            Items = suppliers.Select(x => new Supplier.Search.Result.Item().CopyFrom(x, dto)).ToList(),
            Total = suppliers.Select(x => x.Total).FirstOrDefault()
        };
    }
    
    public Supplier Get(int id)
    {
        var supplier = Check(id);

        return new Supplier().CopyFrom(supplier, dto);
    }

    public void Delete(Delete delete)
    {
        Check(delete.Id);

        var request = new Db.Supplier.Update
        {
            Id = delete.Id,
            Active = false
        };
        request.UpdateList = [nameof(request.Active)];
        request.Exec(sql);
    }

    private Db.Supplier.List.Result Check(int id)
    {
        var supplier = new Db.Supplier.List(id).Exec(sql).FirstOrDefault();

        if (supplier == null)
            throw new RecordNotFoundApiException($"Supplier with id {id} not found.");

        return supplier;
    }
}