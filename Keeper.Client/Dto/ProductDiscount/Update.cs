using System;
using Row.Common1.Client1;

namespace Keeper.Client.ProductDiscount
{
    public partial class Discount
    {
        public class Update
        {
            public int Id { get; set; }
            public double Percent { get; set; }
            public string? Comment { get; set; }
            public DateTimeOffset FromDate { get; set; }
            public DateTimeOffset ToDate { get; set; }

            public Discount Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/update").Body(this);

                return client.ExecuteWithHttp<Discount>(request);
            }

            public Discount ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/update").Body(this);

                return client.ExecuteWithHttp<Discount>(request);
            }
        }
    }
}