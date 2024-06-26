﻿using Row.Common1;
using Row.Common1.Dto1;

namespace Keeper.Api
{
    public class Access : BaseAccess
    {
        public const string Admin = "Admin";
        public const string SystemAdmin = "SystemAdmin";
        public const string SystemAdminOrAdmin = "SystemAdminOrAdmin";
        public const string User = "ActivatedUser";

        public Access()
        {
            Items.Add(new UserAccess(Admin, UserRoles.Admin));
            Items.Add(new UserAccess(SystemAdmin, UserRoles.SystemAdmin, UserRoles.Admin, UserRoles.ActivatedUser));
            Items.Add(new UserAccess(SystemAdminOrAdmin, UserRoles.SystemAdmin, UserRoles.Admin));
            Items.Add(new UserAccess(User, UserRoles.ActivatedUser));
        }
    }
}
