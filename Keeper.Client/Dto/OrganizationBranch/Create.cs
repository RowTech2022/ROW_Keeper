using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class OrganizationBranch
    {
        public class Create
        {
            public int OwnerId { get; set; }

            [Required]
            [TrimWhitespace(500)]
            public string BranchName { get; set; } = null!;

            [Required]
            [TrimWhitespace(20)]
            public string BranchPhone { get; set; } = null!;

            [TrimWhitespace(100)]
            public string? BranchEmail { get; set; }

            [Required]
            [TrimWhitespace(500)]
            public string BranchAddress { get; set; } = null!;
            
            public Organization Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/create").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }

            public Organization ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/create").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }
        }
    }
}