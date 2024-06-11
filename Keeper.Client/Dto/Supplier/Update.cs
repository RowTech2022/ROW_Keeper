using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Supplier
    {
        public class Update
        {
            public int Id { get; set; }
            
            [TrimWhitespace(500)]
            public string CompanyName { get; set; } = null!;

            [TrimWhitespace(500)]
            public string Phone { get; set; } = null!;

            [TrimWhitespace(100)]
            public string? Email { get; set; }

            [TrimWhitespace(300)]
            public string? Address { get; set; }

            public Supplier Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/supplier/update").Body(this);

                return client.ExecuteWithHttp<Supplier>(request);
            }

            public Supplier ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/supplier/update").Body(this);

                return client.ExecuteWithHttp<Supplier>(request);
            }
        }
    }
}