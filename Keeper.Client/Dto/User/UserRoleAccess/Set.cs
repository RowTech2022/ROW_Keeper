using Row.Common1.Client1;
using Row.Common1.Dto1;

namespace Keeper.Client
{
    public partial class User
    {
        public partial class UserRoleAccess
        {
            public class Set
            {
                public int UserId { get; set; }
                public UserRoles RoleId { get; set; }

                public Set() { }
                public Set(int userId, UserRoles role)
                {
                    UserId = userId;
                    RoleId = role;
                }

                public void Exec(KeeperApiClient client)
                {
                    var req = client.PostRequest("api/user/setrole").Body(this);

                    client.Execute(req);
                }

                public void ExecTest(KeeperApiClient client)
                {
                    var req = client.PostRequest("api/user/setrole").Body(this);

                    client.ExecuteWithHttp(req);
                }
            }
        }
    }
}
