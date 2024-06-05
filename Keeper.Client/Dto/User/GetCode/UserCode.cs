using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class User
    {
        public class UserCode
        {
            [Required]
            [TrimWhitespace] 
            public string Login { get; set; } = null!;

            public class Result
            {
                public int ResultCode { get; set; }
            }

            public void Exec(KeeperApiClient client)
            {
                var req = client.PostRequest($"auth/getcode").Body(this);
                client.Execute(req);
            }

            public void ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest($"auth/getcode").Body(this);
                client.ExecuteWithHttp(req);
            }
        }
    }
}
