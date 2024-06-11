using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Supplier
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
        }

        public Supplier Exec(KeeperApiClient client)
        {
            var request = client.PostRequest("api/supplier/search").Body(this);

            return client.ExecuteWithHttp<Supplier>(request);
        }

        public Supplier ExecTest(KeeperApiClient client)
        {
            var request = client.PostRequest("api/supplier/search").Body(this);

            return client.ExecuteWithHttp<Supplier>(request);
        }
    }
}