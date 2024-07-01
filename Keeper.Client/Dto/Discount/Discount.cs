using System;

namespace Keeper.Client.ProductDiscount
{
    public partial class Discount : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UPC { get; set; } = null!;
        public double Percent { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public DiscountType Type { get; set; }

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