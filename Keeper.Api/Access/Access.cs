using Row.Common1;
using Row.Common1.Dto1;

namespace Keeper.Api
{
    public class Access : BaseAccess
    {
        public const string Admin = "SysAdmin";
        public const string SystemAdmin = "SystemAdmin";
        public const string SystemAdminOrAdmin = "SystemAdminOrAdmin";

        public Access()
        {
            Items.Add(new UserAccess(Admin, UserRoles.Admin));
            Items.Add(new UserAccess(SystemAdmin, UserRoles.SystemAdmin));
            Items.Add(new UserAccess(SystemAdminOrAdmin, UserRoles.SystemAdmin, UserRoles.Admin));
        }
    }
}
