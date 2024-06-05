using Bibliotekaen.Dto;
using Microsoft.EntityFrameworkCore.Update;

namespace Keeper.Core;

[DtoContainer]
public static class UserDto
{
    public interface INameSection
    {
        string Name { get; set; }
        string Surname { get; set; }
    }
    
    public interface IEmail
    {
        string? Email { get; set; }
    }
    
    public interface IClientSearch
    {
        Client.User.Search.Filter Filters { get; set; }
    }
    
    public class Client_Create : Client.User.Create, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin
    { }
    
    public class Db_Create : Db.User.Create, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin
    { }
    
    public class Client_Update : Client.User.Update, MainDto.IId, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin
    { }
    
    public class Db_Update : Db.User.Update, MainDto.IId, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin
    { }
    
    public class Client_Search : Client.User.Search, MainDto.IPageInfoSource, IClientSearch
    { }
    
    public class Db_Search : Db.User.Search, MainDto.IPageInfoDb
    { }
    
    public class Client_User : Client.User, MainDto.IId, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin, MainDto.ILife, MainDto.ITimestamp
    { }
    
    public class Db_User_List : Db.User.List.Result, MainDto.IId, INameSection, IEmail, MainDto.IUserType, MainDto.IPhone, MainDto.ILogin, MainDto.ILife, MainDto.ITimestamp
    { }
}