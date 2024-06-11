using Bibliotekaen.Dto;
using Keeper.Client.Product;

namespace Keeper.Core;

[DtoContainer]
public static class ProductDto
{
    public interface ISupplierId
    {
        int SupplierId { get; set; }
    }

    public interface IName
    {
        string Name { get; set; }
    }

    public interface ICategoryId
    {
        int CategoryId { get; set; }
    }
    
    public interface ICategoryName
    {
        string CategoryName { get; set; }
    }

    public interface ITaxId
    {
        int TaxId { get; set; }
    }

    public interface IUPC
    {
        string UPC { get; set; }
    }

    public interface IAgeLimit
    {
        int AgeLimit { get; set; }
    }

    public interface IQuantity
    {
        int Quantity { get; set; }
    }

    public interface IBuyingPrice
    {
        decimal BuyingPrice { get; set; }
    }

    public interface IPrice
    {
        decimal Price { get; set; }
    }

    public interface IDiscountPrice
    {
        decimal DiscountPrice { get; set; }
    }

    public interface ITotalPrice
    {
        decimal TotalPrice { get; set; }
    }

    public interface IMargin
    {
        int Margin { get; set; }
    }

    public interface IHaveDiscount
    {
        bool HaveDiscount { get; set; }
    }

    public interface IExpireDate
    {
        DateTimeOffset? ExpiredDate { get; set; }
    }
    
    public interface INameOrUPC
    {
        string? NameOrUPC { get; set; }
    }
    
    public interface IFilter
    {
        Product.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : INameOrUPC;

    #region IDbFilter

    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto)
    {
        dst.CopyFrom(src.Filters, dto);
    }

    #endregion

    public class Client_Create : Product.Create, ISupplierId, ICategoryId, ITaxId, IUPC, IName,
        IAgeLimit, IQuantity, IBuyingPrice, IPrice, IDiscountPrice, ITotalPrice, IMargin, IHaveDiscount, IExpireDate;

    public class Db_Create : Db.Product.Create, MainDto.IBranchId, ISupplierId, ICategoryId, ITaxId, IUPC, IName,
        IAgeLimit, IQuantity, IBuyingPrice, IPrice, IDiscountPrice, ITotalPrice, IMargin, IHaveDiscount, IExpireDate;

    public class Client_Udpate : Product.Update, MainDto.IId, ICategoryId, ITaxId, IUPC, IName, IAgeLimit, IQuantity, IBuyingPrice,
        IPrice, IDiscountPrice, ITotalPrice, IMargin, IHaveDiscount, IExpireDate;

    public class Db_Udpate : Db.Product.Update, MainDto.IId, ICategoryId, ITaxId, IUPC, IName, IAgeLimit, IQuantity, IBuyingPrice,
        IPrice, IDiscountPrice, ITotalPrice, IMargin, IHaveDiscount, IExpireDate;

    public class Client_Product : Product, MainDto.IId, MainDto.IBranchId, ISupplierId, ICategoryName, ITaxId, IUPC,
        IName, IAgeLimit, IQuantity, IBuyingPrice, IPrice, IDiscountPrice, ITotalPrice, IMargin, IHaveDiscount,
        IExpireDate, MainDto.IActive, MainDto.ILife, MainDto.ITimestamp;

    public class Db_List_Result : Db.Product.List.Result, MainDto.IId, MainDto.IBranchId, ISupplierId, ICategoryName,
        ITaxId, IUPC, IName, IAgeLimit, IQuantity, IBuyingPrice, IPrice, IDiscountPrice, ITotalPrice, IMargin, 
        IHaveDiscount, IExpireDate, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search_Filter : Product.Search.Filter, INameOrUPC;

    public class Client_Search : Product.Search, IFilter, MainDto.IPageInfoSource;

    public class Client_Search_Result_Item : Product.Search.Result.Item, MainDto.IId, ICategoryName, IUPC, IName,
        IAgeLimit, IQuantity, IPrice, IDiscountPrice, IHaveDiscount, IExpireDate;

    public class Db_Search : Db.Product.Search, INameOrUPC, MainDto.IPageInfoDb;
    
    public class Db_Search_Result : Db.Product.Search.Result, MainDto.IId, ICategoryName, IUPC, IName,
        IAgeLimit, IQuantity, IPrice, IDiscountPrice, IHaveDiscount, IExpireDate;
}