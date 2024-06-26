using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client.ProductDiscount
{
    public partial class Discount
    {
        public partial class Search
        {
            public int[]? Ids { get; set; }
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();

            public Result Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/search").Body(this);

                return client.ExecuteWithHttp<Result>(request);
            }

            public Result ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/search").Body(this);

                return client.ExecuteWithHttp<Result>(request);
            }
        }
    }
}