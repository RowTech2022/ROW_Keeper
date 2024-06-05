using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class User
    {
        public class CheckCode
        {
            public int Code { get; set; }
            
            [Required]
            [TrimWhitespace(50)]
            public string Login { get; set; } = null!;

            public void Exec(KeeperApiClient client)
            {
                var req = client.PostRequest($"auth/checkCode").Body(this);
                client.Execute(req);
            }

            public bool ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest($"auth/checkCode").Body(this);
                return client.Execute<bool>(req);
            }
        }
    }
}
