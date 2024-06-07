using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Organization
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
            
            public Result Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/search").Body(this);

                return client.ExecuteWithHttp<Result>(request);
            }

            public Result ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/organization/search").Body(this);

                return client.ExecuteWithHttp<Result>(request);
            }
        }
    }
}