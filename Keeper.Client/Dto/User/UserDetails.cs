using Row.Common1.Dto1;
using System;
using System.Collections.Generic;


namespace Keeper.Client
{
    public partial class User
    {
        public class UserDetails
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string SurName { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Login { get; set; } = null!;
            public UserType UserType { get; set; }
            public List<UserRoles> Roles { get; set; } = null!;

            public static UserDetails Exec(KeeperApiClient client)
            {
                var req = client.GetRequest("api/User/UserDetails");

                return client.Execute<UserDetails>(req);
            }

            public static UserDetails ExecTest( KeeperApiClient client)
            {
                var req = client.GetRequest("api/User/UserDetails");

                return client.ExecuteWithHttp<UserDetails>(req);
            }
        }
    }
}