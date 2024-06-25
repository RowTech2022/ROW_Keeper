using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class OrganizationDto
{
    public interface IOrgName
    {
        string OrgName { get; set; }
    }
    
    public interface IOrgDescription
    {
        string? OrgDescription { get; set; }
    }
    
    public interface IOrgPhone
    {
        string OrgPhone { get; set; }
    }
    
    public interface IOrgEmail
    {
        string? OrgEmail { get; set; }
    }
    
    public interface IOrgAddress
    {
        string OrgAddress { get; set; }
    }
    
    public interface IFilter
    {
        Organization.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IOrgName;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion
    
    public interface IOwnerFullName
    {
        string OwnerFullName { get; set; }
    }
    
    public interface IOwnerEmail
    {
        string? OwnerEmail { get; set; }
    }
    
    public interface IOwnerPhone
    {
        string OwnerPhone { get; set; }
    }
    
    public interface IPlanId
    {
        int? PlanId { get; set; }
    }

    public interface IPlaneName
    {
        string? PlanName { get; set; }
    }
    
    public interface IInformation
    {
        Information? Plan { get; set; }
    }

    public interface IDbInformation : IPlanId, IPlaneName;

    #region IDbInformation

    [DtoConvert]
    static void Convert(IInformation dst, IDbInformation src)
    {
        dst.Plan = new Information
        {
            Id = src.PlanId,
            Value = src.PlanName
        };
    }

    #endregion
    
    public interface IStatus
    {
        Organization.OrgStatus Status { get; set; }
    }
    
    public class Client_Create : Organization.Create, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus;
    
    public class Db_Create : Db.Organization.Create, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus;
    
    public class Client_Update : Organization.Update, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, MainDto.ITimestamp, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus;
    
    public class Db_Update : Db.Organization.Update, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus;
    
    public class Client_Get : Organization, MainDto.IId, IInformation, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus, MainDto.ILife, MainDto.ITimestamp;
    
    public class Db_List_Result : Db.Organization.List.Result, MainDto.IId, IDbInformation, MainDto.IOwnerId, IOrgName, IOrgDescription, IOrgPhone, IOrgEmail, IOrgAddress, IOwnerFullName, IOwnerEmail, IOwnerPhone, IStatus, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Organization.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;
    
    public class Client_Search_Filter : Organization.Search.Filter, IOrgName;

    public class Client_Search_Result : Organization.Search.Result.Item, MainDto.IId, IInformation, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress, IStatus;

    public class Db_Search : Db.Organization.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds;
    
    public class Db_Search_Result : Db.Organization.Search.Result, MainDto.IId, IDbInformation, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress, IStatus;
}