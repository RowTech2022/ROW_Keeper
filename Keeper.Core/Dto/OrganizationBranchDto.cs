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
    
    public interface IBranchEmail
    {
        string? BranchEmail { get; set; }
    }
    
    public interface IBranchAddress
    {
        string BranchAddress { get; set; }
    }
    
    public interface IFilter
    {
        OrganizationBranch.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IBranchName;

    #region IDbFilter

    [DtoConvert]
    static void Convert(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : OrganizationBranch.Create, MainDto.IOwnerId, IBranchName, IBranchEmail, IBranchPhone, IBranchAddress;
    
    public class Db_Create : Db.OrganizationBranch.Create, MainDto.IOwnerId, IBranchName, IBranchEmail, IBranchPhone, IBranchAddress;

    public class Client_Update : OrganizationBranch.Update, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchEmail, IBranchPhone, IBranchAddress;
    
    public class Db_Update : Db.OrganizationBranch.Update, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchEmail, IBranchPhone, IBranchAddress;
    
    public class Client_Get : OrganizationBranch, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchEmail, IBranchAddress, MainDto.ILife, MainDto.ITimestamp;
    
    public class Db_List_Result : Db.OrganizationBranch.List.Result, MainDto.IId, MainDto.IOwnerId, IBranchName, IBranchPhone, IBranchEmail, IBranchAddress, MainDto.ILife, MainDto.ITimestamp;

    public class Client_Search : OrganizationBranch.Search, IFilter, MainDto.IPageInfoSource, MainDto.IIds;

    public class Client_Search_Filter : OrganizationBranch.Search.Filter, IBranchName;
    
    public class Client_Search_Result_Item : OrganizationBranch.Search.Result.Item, MainDto.IId, IBranchName, IBranchPhone, IBranchEmail, IBranchAddress;

    public class Db_Search : Db.OrganizationBranch.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds;
    
    public class Db_Search_Result : Db.OrganizationBranch.Search.Result, MainDto.IId, IBranchName, IBranchPhone, IBranchEmail, IBranchAddress; 
}