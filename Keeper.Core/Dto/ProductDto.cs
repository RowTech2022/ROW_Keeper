using Bibliotekaen.Dto;
using Keeper.Client;
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

    public interface ISubCategoryId
    {
        int SubCategoryId { get; set; }
    }

    public interface ISubCategoryName
    {
        string SubCategoryName { get; set; }
    }

    public interface ICategory
    {
        Information Category { get; set; }
    }

    public interface IDbCategory : ICategoryId, ICategoryName;

    #region IDbCategory

    static void Convert(ICategory dstCat, IDbCategory srcCat, ISubCategory? dstSub, IDbSubCategory srcSub )
    {
        if (srcCat.CategoryId == 0)
        {
            dstCat.Category = new Information
            {
                Id = srcSub.SubCategoryId,
                Value = srcSub.SubCategoryName
            };
            dstSub = null;
        }
        else
        {
            dstCat.Category = new Information
            {
                Id = srcCat.CategoryId,
                Value = srcCat.CategoryName
            };
            
            dstSub.SubCategory = new Information
            {
                Id = srcSub.SubCategoryId,
                Value = srcSub.SubCategoryName
            };
        }
    }

    #endregion

    public interface ISubCategory
    {
        Information? SubCategory { get; set; }
    }

    public interface IDbSubCategory : ISubCategoryId, ISubCategoryName;

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

    public interface ITotalPrice
    {
        decimal TotalPrice { get; set; }
    }

    public interface IMargin
    {
        int Margin { get; set; }
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

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public interface ICategroyIds
    {
        int[]? CategoryIds { get; set; }
    }

    public class Client_Create : Product.Create, ISupplierId, ICategoryId, ITaxId, IUPC, IName,
        IAgeLimit, IQuantity, IBuyingPrice, IPrice, ITotalPrice, IMargin, IExpireDate;

    public class Db_Create : Db.Product.Create, MainDto.IOrgId, ISupplierId, ICategoryId, ITaxId, IUPC, IName,
        IAgeLimit, IQuantity, IBuyingPrice, IPrice, ITotalPrice, IMargin, IExpireDate;

    public class Client_Udpate : Product.Update, MainDto.IId, ICategoryId, ITaxId, IUPC, IName, IAgeLimit, IQuantity,
        IBuyingPrice,
        IPrice, ITotalPrice, IMargin, IExpireDate;

    public class Db_Udpate : Db.Product.Update, MainDto.IId, ICategoryId, ITaxId, IUPC, IName, IAgeLimit, IQuantity,
        IBuyingPrice,
        IPrice, ITotalPrice, IMargin, IExpireDate;

    public class Client_Product : Product, MainDto.IId, MainDto.IOrgId, ISupplierId, ICategoryName, ITaxId, IUPC,
        IName, IAgeLimit, IQuantity, IBuyingPrice, IPrice, ITotalPrice, IMargin,
        IExpireDate, MainDto.ILife, MainDto.ITimestamp;

    public class Db_List_Result : Db.Product.List.Result, MainDto.IId, MainDto.IOrgId, ISupplierId, ICategoryName,
        ITaxId, IUPC, IName, IAgeLimit, IQuantity, IBuyingPrice, IPrice, ITotalPrice, IMargin, IExpireDate,
        MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search_Filter : Product.Search.Filter, INameOrUPC, ICategroyIds;

    public class Client_Search : Product.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Result_Item : Product.Search.Result.Item, MainDto.IId, ICategoryName, IUPC, IName,
        IAgeLimit, IQuantity, IPrice, IExpireDate;

    public class Db_Search : Db.Product.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds, ICategroyIds;

    public class Db_Search_Result : Db.Product.Search.Result, MainDto.IId, ICategoryName, IUPC, IName,
        IAgeLimit, IQuantity, IPrice, IExpireDate;
}