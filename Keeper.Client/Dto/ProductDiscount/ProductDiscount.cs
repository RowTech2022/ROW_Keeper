using System;

namespace Keeper.Client.ProductDiscount
{
    public partial class Discount : BaseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string UPC { get; set; } = null!;
        public double Percent { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public string Category { get; set; } = null!;
        public string SubCategory { get; set; } = null!;

        public static Discount Exec(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/productDiscount/get/{id}");

            return client.ExecuteWithHttp<Discount>(request);
        }

        public static Discount ExecTest(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/productDiscount/get/{id}");

            return client.ExecuteWithHttp<Discount>(request);
        }
    }
}