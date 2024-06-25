using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client;

public partial class Plan
{
    public partial class Search
    {
        public int[]? Ids { get; set; }
        public Filter Filters { get; set; } = new Filter();
        public PageInfo PageInfo { get; set; } = new PageInfo();

        public Plan Exec(KeeperApiClient client)
        {
            var request = client.PostRequest("api/plan/search").Body(this);

            return client.ExecuteWithHttp<Plan>(request);
        }

        public Result ExecTest(KeeperApiClient client)
        {
            var request = client.PostRequest("api/plan/search").Body(this);

            return client.ExecuteWithHttp<Result>(request);
        }
    }
}