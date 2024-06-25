using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class SubscriptionDto
{
    public interface IPlanId
    {
        int PlanId { get; set; }
    }

    public interface IPlanName
    {
        string PlanName { get; set; }
    }

    public interface IOrgName
    {
        string OrgName { get; set; }
    }

    public interface IDuration
    {
        DateTimeOffset StartDate { get; set; }
        DateTimeOffset EndDate { get; set; }
    }

    public interface IOrgInformation
    {
        Information Organization { get; set; }
    }

    public interface IDbOrgInformation : MainDto.IOrgId, IOrgName;

    #region IDbOrgInformation

    [DtoConvert]
    static void Convert(IOrgInformation dst, IDbOrgInformation src) =>
        dst.Organization = new Information { Id = src.OrgId, Value = src.OrgName };

    #endregion
    
    public interface IPlanInformation
    {
        Information Plan { get; set; }
    }

    public interface IDbPlanInformation : IPlanId, IPlanName;

    #region IDbPlanInformation

    [DtoConvert]
    static void Convert(IPlanInformation dst, IDbPlanInformation src) =>
        dst.Plan = new Information { Id = src.PlanId, Value = src.PlanName };

    #endregion
    
    public interface IFilter
    {
        Subscription.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IOrgName;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : Subscription.Create, MainDto.IOrgId, IPlanId;

    public class Db_Create : Db.Subscription.Create, MainDto.IOrgId, IPlanId;

    public class Client_Update : Subscription.Update, IDuration;

    public class Db_Update : Db.Subscription.Update, IDuration;

    public class Client_Subscription : Subscription, MainDto.IId, IOrgInformation, IPlanInformation, IDuration,
        MainDto.ILife, MainDto.ITimestamp;

    public class Db_List_Result : Db.Subscription.List.Result, MainDto.IId, MainDto.IOrgId, IOrgName, IPlanId,
        IPlanName, IDuration, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Subscription.Search, MainDto.IIds, IFilter, MainDto.IPageInfoSource;

    public class Client_Search_Fitler : Subscription.Search.Filter, IOrgName;

    public class Client_Search_Result_Item : Subscription.Search.Result.Item, MainDto.IId, IOrgInformation,
        IPlanInformation, IDuration;

    public class Db_Search : Db.Subscription.Search, IDbFilter, MainDto.IIds, MainDto.IPageInfoDb;

    public class Db_Search_Result : Db.Subscription.Search.Result, MainDto.IId, MainDto.IOrgId, IOrgName, IPlanId,
        IPlanName, IDuration;
}