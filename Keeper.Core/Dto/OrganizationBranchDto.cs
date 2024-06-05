using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class OrganizationBranchDto
{
    public interface IBranchName
    {
        string BranchName { get; set; }
    }
    
    public interface IBranchPhone
    {
        string BranchPhone { get; set; }
    }
    
    public interface IBranchAddress
    {
        string BranchAddress { get; set; }
    }
    
    public class Client_Create : OrganizationBranch.Create, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchAddress
    { }
    
    public class Db_Create : Db.OrganizationBranch.Create, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchAddress
    { }
    
    public class Client_Get : OrganizationBranch, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchAddress, MainDto.ILife, MainDto.ITimestamp
    { }
    
    public class Db_List_Result : Db.OrganizationBranch.List.Result, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchAddress, MainDto.ILife, MainDto.ITimestamp
    { }
}