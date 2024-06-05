using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class User
    {
        public class UpdatePassword
        {
            public string OldPassWord { get; set; } = null!;
            public string NewPassWord { get; set; } = null!;

            public byte[]? TimeStamp { get; set; }

            public void Exec(KeeperApiClient client)
            {
                var req = client.PostRequest("api/user/changePassword").Body(this);

                client.Execute(req);
            }

            public void ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest("api/user/changePassword").Body(this);

                client.ExecuteWithHttp(req);
            }
        }
    }
}
