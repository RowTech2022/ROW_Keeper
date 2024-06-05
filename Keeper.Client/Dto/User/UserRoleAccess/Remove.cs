using Row.Common1.Client1;
using Row.Common1.Dto1;

namespace Keeper.Client
{
    public partial class User
    {
        public partial class UserRoleAccess
        {
            public class Remove
            {
                public int UserId { get; set; }
                public UserRoles RoleId { get; set; }

                public void Exec(KeeperApiClient client)
                {
                    var req = client.PostRequest("api/user/removerole").Body(this);

                    client.Execute(req);
                }

                public void ExecTest(KeeperApiClient client)
                {
                    var req = client.PostRequest("api/user/removerole").Body(this);

                    client.ExecuteWithHttp<User>(req);
                }
            }
        }
    }
}
