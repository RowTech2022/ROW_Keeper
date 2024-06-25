using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class PlanDto
{
    public interface IName
    {
        string Name { get; set; }
    }
    
    public interface IPrice
    {
        decimal Price { get; set; }
    }
    
    public interface IDuration
    {
        int Duration { get; set; }
    }
    
    public interface IType
    {
        Plan.PlanType Type { get; set; }
    }
    
    public interface IFilter
    {
        Plan.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IName;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : Plan.Create, IName, IPrice, IDuration, IType;

    public class Db_Create : Db.Plan.Create, IName, IPrice, IDuration, IType;

    public class Client_Update : Plan.Update, MainDto.IId, IName, IPrice, IDuration, IType;

    public class Db_Update : Db.Plan.Update, MainDto.IId, IName, IPrice, IDuration, IType;

    public class Client_Plan : Plan, MainDto.IId, IName, IPrice, IDuration, IType, MainDto.ILife, MainDto.ITimestamp;
    
    public class Db_List_Result : Db.Plan.List.Result, MainDto.IId, IName, IPrice, IDuration, IType, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Plan.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Filter : Plan.Search.Filter, IName;

    public class Client_Search_Result_Item : Plan.Search.Result.Item, MainDto.IId, IName, IPrice, IDuration, IType;

    public class Db_Search : Db.Plan.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds;

    public class Db_Search_Result : Db.Plan.Search.Result, MainDto.IId, IName, IPrice, IDuration, IType;
}