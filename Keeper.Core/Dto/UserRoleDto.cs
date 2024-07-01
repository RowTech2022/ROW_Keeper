using Bibliotekaen.Dto;
using Keeper.Client;

namespace Keeper.Core;

[DtoContainer]
public static class UserRoleDto
{
    public interface IRoleId
    {
        int RoleId { get; set; }
    }
    
    public interface IRoleName
    {
        string RoleName { get; set; }
    }

    public class Client_User_Role_List_Item : User.Role.List.Item, IRoleId, IRoleName;
    
    public class Db_User_Role_List_Result : Db.User.Role.List.Result, IRoleId, IRoleName;
}