using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class CategoryDto
{
    public interface IParentId
    {
        int ParentId { get; set; }
    }

    public interface IName
    {
        string Name { get; set; }
    }

    public interface IDescription
    {
        string? Description { get; set; }
    }
    
    public interface IFilter
    {
        Category.Search.Filter Filters { get; set; }
    }

    public class Client_Create : Category.Create, IParentId, IName, IDescription;

    public class Db_Create : Db.Category.Create, IParentId, IName, IDescription;

    public class Client_Update : Category.Update, MainDto.IId, IParentId, IName, IDescription;

    public class Db_Update : Db.Category.Update, MainDto.IId, IParentId, IName, IDescription;

    public class Client_Category : Category, MainDto.IId, MainDto.IOrgId, IParentId, IName, IDescription, MainDto.IActive,
        MainDto.ILife, MainDto.ITimestamp;

    public class Db_List_Result : Db.Category.List.Result, MainDto.IId, MainDto.IOrgId, IParentId, IName, IDescription,
        MainDto.IActive, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Category.Search, IFilter, MainDto.IPageInfoSource;

    public class Client_Search_Filter : Category.Search.Filter, IName;

    public class Client_Search_Result_Item : Category.Search.Result.Item, MainDto.IId, IName, IDescription;

    public class Db_Search_Result : Db.Category.Search.Result, MainDto.IId, IParentId, IName, IDescription;
}