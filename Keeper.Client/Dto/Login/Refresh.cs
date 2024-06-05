using System.ComponentModel.DataAnnotations;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public class Refresh
    {
        [Required]
        public string RefreshToken { get; set; } = null!;

        public TokenInfo Exec(KeeperApiClient client)
        {
            var req = client.PostRequest("auth/refresh").Body(this);

            return client.Execute<TokenInfo>(req);
        }

        public TokenInfo ExecTest(KeeperApiClient client)
        {
            var req = client.PostRequest("auth/refresh").Body(this);

            return client.ExecuteWithHttp<TokenInfo>(req);
        }
    }
}
