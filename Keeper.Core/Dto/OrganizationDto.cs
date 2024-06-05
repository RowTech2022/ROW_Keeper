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
    
    public interface IOrgAddress
    {
        string OrgAddress { get; set; }
    }
    
    public class Client_Create : Organization.Create, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgAddress
    { }
    
    public class Db_Create : Db.Organization.Create, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgAddress
    { }
    
    public class Client_Get : Organization, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgAddress, MainDto.ILife, MainDto.ITimestamp
    { }
    
    public class Db_List_Result : Db.Organization.List.Result, MainDto.IId, MainDto.IOwnerId, IOrgName, IOrgPhone, IOrgAddress, MainDto.ILife, MainDto.ITimestamp
    { }
}