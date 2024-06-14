using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class SupplierDto
{
    public interface ICompanyName
    {
        string CompanyName { get; set; }
    }
    
    public interface IFilter
    {
        Supplier.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : ICompanyName;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : Supplier.Create, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;
    
    public class Db_Create : Db.Supplier.Create, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;

    public class Client_Update : Supplier.Update, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;
    
    public class Db_Update : Db.Supplier.Update, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;
    
    public class Client_Supplier : Supplier, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress, MainDto.ILife, MainDto.ITimestamp;
    
    public class Db_List_Result : Db.Supplier.List.Result, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Supplier.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;
    
    public class Client_Search_Filter : Supplier.Search.Filter, ICompanyName;
    
    public class Client_Search_Result : Supplier.Search.Result.Item, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;

    public class Db_Search : Db.Supplier.Search, MainDto.IIds, IDbFilter, MainDto.IPageInfoDb;
    
    public class Db_Search_Result : Db.Supplier.Search.Result, MainDto.IId, ICompanyName, MainDto.IPhone, MainDto.IEmail, MainDto.IAddress;
}