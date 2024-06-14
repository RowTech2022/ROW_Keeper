using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class CategoryDto
{
    public interface IParentId
    {
        int? ParentId { get; set; }
    }

    public interface IName
    {
        string Name { get; set; }
    }
    
    public interface ISubCategorySection
    {
        string CategoryName { get; set; }
        string SubCategoryName { get; set; }
    }
    
    public interface IFilter
    {
        Category.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IName;

    #region DbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : Category.Create, IParentId, IName;

    public class Db_Create : Db.Category.Create, IParentId, IName;

    public class Client_Update : Category.Update, MainDto.IId, IParentId, IName;

    public class Db_Update : Db.Category.Update, MainDto.IId, IParentId, IName;

    public class Client_Category : Category, MainDto.IId, MainDto.IOrgId, IParentId, IName,
        MainDto.ILife, MainDto.ITimestamp;

    public class Db_List_Result : Db.Category.List.Result, MainDto.IId, MainDto.IOrgId, IParentId, IName,
        MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Category.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Filter : Category.Search.Filter, IName;

    public class Client_Search_CategoryResult_Item : Category.Search.CategoryResult.Item, MainDto.IId, IName;

    public class Client_Search_SubCategoryResult_Item : Category.Search.SubCategoryResult.Item, MainDto.IId, ISubCategorySection;

    public class Db_Search : Db.Category.Search, MainDto.IIds, MainDto.IPageInfoDb, IDbFilter;

    public class Db_Search_Result : Db.Category.Search.Result, MainDto.IId, IName;

    public class Db_SearchSubCategory : Db.Category.SearchSubCategory, MainDto.IIds, MainDto.IPageInfoDb, IDbFilter;
    
    public class Db_SearchSubCategory_Result : Db.Category.SearchSubCategory.Result, MainDto.IId, ISubCategorySection;
}