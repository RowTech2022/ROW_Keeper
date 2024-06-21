using Bibliotekaen.Dto;
using Keeper.Client;
using Microsoft.EntityFrameworkCore.Update;

namespace Keeper.Core;

[DtoContainer]
public static class UserDto
{
    public interface IFullName
    {
        string FullName { get; set; }
    }

    public interface IEmail
    {
        string? Email { get; set; }
    }

    public interface IFilter
    {
        Client.User.Search.Filter Filters { get; set; }
    }

    public interface IDbFilter : IEmail, MainDto.ILogin;

    #region IDbFilter

    [DtoConvert]
    static void Converter(IDbFilter dst, IFilter src, DtoComplex dto) => dst.CopyFrom(src.Filters, dto);

    #endregion

    public class Client_Create : User.Create, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin,
        MainDto.IStatus, MainDto.IBranchId;

    public class Db_Create : Db.User.Create, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin,
        MainDto.IStatus, MainDto.IBranchId;

    public class Client_Update : User.Update, MainDto.IId, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone,
        MainDto.ILogin, MainDto.IBranchId;

    public class Db_Update : Db.User.Update, MainDto.IId, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone,
        MainDto.ILogin, MainDto.IBranchId;

    public class Client_Search : User.Search, MainDto.IPageInfoSource, IFilter, MainDto.IIds;

    public class Client_Search_Filter : User.Search.Filter, MainDto.ILogin, IEmail;

    public class Client_Search_Result_Item : User.Search.Result.Item, MainDto.IId, IFullName, IEmail, MainDto.IUserType,
        MainDto.IPhone, MainDto.ILogin, MainDto.IStatus;

    public class Db_Search : Db.User.Search, IDbFilter, MainDto.IPageInfoDb, MainDto.IIds;

    public class Db_Search_Result : Db.User.Search.Result, MainDto.IId, IFullName, IEmail, MainDto.IUserType,
        MainDto.IPhone, MainDto.ILogin, MainDto.IStatus;

    public class Client_User : User, MainDto.IId, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin,
        MainDto.ILife, MainDto.ITimestamp;

    public class Db_User_List : Db.User.List.Result, MainDto.IId, IFullName, IEmail, MainDto.IUserType, MainDto.IPhone,
        MainDto.ILogin, MainDto.ILife, MainDto.ITimestamp;
}