using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class OrganizationBranch
    {
        public partial class Search
        {
            public int[]? Ids { get; set; }
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
            
            public OrganizationBranch Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/search").Body(this);

                return client.ExecuteWithHttp<OrganizationBranch>(request);
            }

            public Result ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organizationBranch/search").Body(this);

                return client.ExecuteWithHttp<Result>(request);
            }
        }
    }
}