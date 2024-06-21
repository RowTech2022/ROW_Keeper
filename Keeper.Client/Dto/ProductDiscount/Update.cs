using System;
using Row.Common1.Client1;

namespace Keeper.Client.ProductDiscount
{
    public partial class ProductDiscount
    {
        public class Update
        {
            public int Id { get; set; }
            public double Percent { get; set; }
            public string? Comment { get; set; }
            public DateTimeOffset FromDate { get; set; }
            public DateTimeOffset ToDate { get; set; }

            public ProductDiscount Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/update").Body(this);

                return client.ExecuteWithHttp<ProductDiscount>(request);
            }

            public ProductDiscount ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/productDiscount/update").Body(this);

                return client.ExecuteWithHttp<ProductDiscount>(request);
            }
        }
    }
}