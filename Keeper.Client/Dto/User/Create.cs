using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;


namespace Keeper.Client
{
    public partial class User
    {
        public class Create
        {
            [Required]
            [TrimWhitespace]
            public string Name { get; set; } = null!;

            [Required]
            [TrimWhitespace]
            public string Surname { get; set; } = null!;

            [Required]
            [TrimWhitespace]
            public string Phone { get; set; } = null!;

            public string? Email  { get; set;}

            public string Login { get; set; } = null!;

            public UserType UserType { get; set; }

            public int BranchId { get; set; }

            public User Exec(KeeperApiClient client)
            {
                var req = client.PostRequest("api/User/create").Body(this);

                return client.Execute<User>(req);
            }

            public User ExecTest(KeeperApiClient client)
            {
                var req = client.PostRequest("api/User/create").Body(this);

                return client.ExecuteWithHttp<User>(req);
            }

            public User ExecTestPublic(KeeperApiClient client)
            {
                var req = client.PostRequest("api/User/createPublic").Body(this);

                return client.ExecuteWithHttp<User>(req);
            }
        }
    }
}