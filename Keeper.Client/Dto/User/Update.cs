using System;
using Row.Common1.Client1;


namespace Keeper.Client
{
    public partial class User
    {
        public class Update
        {
			public int Id { get; set; }
            public int OrgId { get; set; }
            public string FullName { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string? Email { get; set; }
            public string Login { get; set; } = null!;
            public UserType UserType { get; set; }

            public User Exec(KeeperApiClient client)
            {
                var req = client.PostRequest("api/User/update").Body(this);

                return client.Execute<User>(req);
            }

            public User ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest("api/User/update").Body(this);

                return client.ExecuteWithHttp<User>(req);
            }
        }
    }
}