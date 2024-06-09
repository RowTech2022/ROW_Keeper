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
    
    public class Client_Create : Organization.Create, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress;
    
    public class Db_Create : Db.Organization.Create, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress;
    
    public class Client_Update : Organization.Update, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress, MainDto.ITimestamp;
    
    public class Db_Update : Db.Organization.Update, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress;
    
    public class Client_Get : Organization, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress, MainDto.ILife, MainDto.ITimestamp;
    
    public class Db_List_Result : Db.Organization.List.Result, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : Organization.Search, IFilter, MainDto.IPageInfoSource;
    
    public class Client_Search_Filter : Organization.Search.Filter, IOrgName;

    public class Client_Search_Result : Organization.Search.Result.Item, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress;

    public class Db_Search : Db.Organization.Search, IOrgName, MainDto.IPageInfoDb;
    
    public class Db_Search_Result : Db.Organization.Search.Result, MainDto.IId, IOrgName, IOrgPhone, IOrgEmail, IOrgAddress;
}