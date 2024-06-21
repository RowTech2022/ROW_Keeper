using Bibliotekaen.Dto;
using Keeper.Client.ProductDiscount;

namespace Keeper.Core;

[DtoContainer]
public static class ProductDiscountDto
{
    public interface IProductName
    {
        string ProductName { get; set; }
    }
    
    public interface IUPC
    {
        string UPC { get; set; }
    }
    
    public interface IProductId
    {
        int ProductId { get; set; }
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
    
    public interface ICategory
    {
        string Category { get; set; }
    }
    
    public interface ISubCategory
    {
        string SubCategory { get; set; }
    }
    
    public interface IFilter
    {
        ProductDiscount.Search.Filter Filters { get; set; }
    }
    
    public interface IDbFilter : IUPC;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto); 

    #endregion

    public class Client_Create : ProductDiscount.Create, IProductId, IPercent, IComment, ILife;

    public class Db_Create : Db.ProductDiscount.Create, IProductId, IPercent, IComment, ILife;

    public class Client_Update : ProductDiscount.Update, MainDto.IId, IPercent, IComment, ILife;
    
    public class Db_Update : Db.ProductDiscount.Update, MainDto.IId, IPercent, IComment, ILife;

    public class Client_ProductDiscount : ProductDiscount, MainDto.IId, IProductName, IUPC, IPercent, IComment, ILife,
        ICategory, ISubCategory, MainDto.ILife, MainDto.ITimestamp;

    public class Db_List : Db.ProductDiscount.List.Result, MainDto.IId, IProductName, IUPC, IPercent, IComment, ILife,
        ICategory, ISubCategory, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : ProductDiscount.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Filter : ProductDiscount.Search.Filter, IUPC;

    public class Client_Search_Result_Item : ProductDiscount.Search.Result.Item, MainDto.IId, IProductName, IUPC, IPercent,
        IComment, ILife, ICategory, ISubCategory;

    public class Db_Search : Db.ProductDiscount.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds;
    
    public class Db_Search_Result : Db.ProductDiscount.Search.Result, MainDto.IId, IProductName, IUPC, IPercent,
        IComment, ILife, ICategory, ISubCategory;
}