using Bibliotekaen.Dto;
using Keeper.Client;
using Keeper.Client.ProductDiscount;

namespace Keeper.Core;

[DtoContainer]
public static class DiscountDto
{
    public interface IProductName
    {
        string ProductName { get; set; }
    }

    public interface IName
    {
        string Name { get; set; }
    }

    public interface IUPC
    {
        string UPC { get; set; }
    }

    public interface IProductId
    {
        int? ProductId { get; set; }
    }

    public interface IPercent
    {
        double Percent { get; set; }
    }

    public interface IComment
    {
        string? Comment { get; set; }
    }

    public interface ILife
    {
        DateTimeOffset FromDate { get; set; }
        DateTimeOffset ToDate { get; set; }
    }

    public interface IFilter
    {
        Discount.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IUPC;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public interface IType
    {
        DiscountType Type { get; set; }
    }
    
    public interface ICategoryId
    {
        int? CategoryId { get; set; }
    }

    public class Client_Create : Discount.Create, IProductId, ICategoryId, IPercent, IComment, ILife, IType;

    public class Db_Create : Db.Discount.Create, IProductId, ICategoryId, IPercent, IComment, ILife, IType;

    public class Client_Update : Discount.Update, MainDto.IId, IProductId, ICategoryId, IPercent, IComment, ILife, IType;

    public class Db_Update : Db.Discount.Update, MainDto.IId, IPercent, IComment, ILife, IType;

    public class Client_Discount : Discount, MainDto.IId, IName, IUPC, IPercent, IComment, ILife, IType, MainDto.ILife,
        MainDto.ITimestamp;

    public class Db_List : Db.Discount.List.Result, MainDto.IId, IProductName, IPercent, IComment, IType, ILife,
        MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Discount.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Filter : Discount.Search.Filter, IUPC;

    public class Client_Search_Result_Item : Discount.Search.Result.Item, MainDto.IId, IProductName, IUPC, IPercent,
        IComment, ILife, IType;

    public class Db_Search : Db.Discount.Search, MainDto.IPageInfoDb, MainDto.IIds;

    public class Db_Search_Result : Db.Discount.Search.Result, MainDto.IId, IProductName, IPercent,
        IComment, ILife, IType;
}