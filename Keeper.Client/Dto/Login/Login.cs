using Row.Common1.Client1;

namespace Keeper.Client
{
    public class Login
    {
        public string UserLogin { get; set; } = null!;
        public string Password { get; set; } = null!;

        public TokenInfo Exec(KeeperApiClient client)
        {
            var req = client.PostRequest("auth/login").Body(this);

            return client.Execute<TokenInfo>(req);
        }

        public TokenInfo ExecTest(KeeperApiClient client)
        {
            var req = client.PostRequest("auth/login").Body(this);

            return client.ExecuteWithHttp<TokenInfo>(req);
        }
    }
}
