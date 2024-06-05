using Row.Common.Dto1;
using Row.Common1.Client1;


namespace Keeper.Client
{
    public partial class User
    {
        public partial class Search
        {
            public int[]? Ids { get; set; }

            public Filter Filters { get; set; } = new Filter();
             public OrderByInfo OrderBy { get; set; } = new OrderByInfo();
             public PageInfo PageInfo { get; set; } = new PageInfo();

             public Result Exec(KeeperApiClient client)
             {
                 var req = client.PostRequest("api/User/search").Body(this);

                 return client.Execute<Result>(req);
             }

             public Result ExecTest(KeeperApiClient client)
             {
                 var req = client.PostRequest("api/User/search").Body(this);

                 return client.ExecuteWithHttp<Result>(req);
             }
        }
    }
}