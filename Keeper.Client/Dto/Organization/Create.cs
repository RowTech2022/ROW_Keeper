using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Organization
    {
        public class Create
        {
            public int OwnerId { get; set; }

            public int PlanId { get; set; }

            [Required]
            [TrimWhitespace(500)]
            public string OrgName { get; set; } = null!;

            [TrimWhitespace(500)]
            public string? OrgDescription { get; set; } = null!;

            [Required]
            [TrimWhitespace(20)]
            public string OrgPhone { get; set; } = null!;

            [TrimWhitespace(100)]
            public string? OrgEmail { get; set; }

            [Required]
            [TrimWhitespace(500)]
            public string OrgAddress { get; set; } = null!;

            [Required]
            [TrimWhitespace(100)]
            public string OwnerFullName { get; set; } = null!;

            [TrimWhitespace(100)]
            public string? OwnerEmail { get; set; } = null!;

            [Required]
            [TrimWhitespace(20)]
            public string OwnerPhone { get; set; } = null!;

            public OrgStatus Status { get; set; }

            public Organization Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/create").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }

            public Organization ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/create").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }
        }
    }
}