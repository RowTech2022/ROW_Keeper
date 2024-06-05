using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class User
    {
        public class UpdatePasswordAdmin
        {
            public int UserId { get; set; }

            //public string? OldPassWord { get; set; }
            public string? NewPassWord { get; set; }

            public byte[]? TimeStamp { get; set; }

            public void Exec(KeeperApiClient client)
            {
                var req = client.PostRequest("api/user/changePasswordAdmin").Body(this);

                client.Execute(req);
            }

            public void ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest("api/user/changePasswordAdmin").Body(this);

                client.ExecuteWithHttp(req);
            }
        }
    }
}
