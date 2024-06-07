using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class OrganizationBranch
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
            
            public Organization Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/search").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }

            public Organization ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/search").Body(this);

                return client.ExecuteWithHttp<Organization>(request);
            }
        }
    }
}