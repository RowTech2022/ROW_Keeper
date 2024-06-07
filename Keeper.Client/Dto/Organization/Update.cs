using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Organization
    {
        public class Update
        {
            public int Id { get; set; }
            
            public int OwnerId { get; set; }

            [Required]
            [TrimWhitespace(500)]
            public string OrgName  { get; set;} = null!;

            [Required]
            [TrimWhitespace(20)]
            public string OrgPhone { get; set;} = null!;

            [TrimWhitespace(100)]
            public string? OrgEmail { get; set;}

            [Required]
            [TrimWhitespace(500)] 
            public string OrgAddress { get; set; } = null!;

            public byte[] Timestamp { get; set; } = null!;
            
            public Organization Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/update").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }

            public Organization ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/update").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }
        }
    }
}