using System;

namespace Keeper.Client.Product
{
    public partial class Product
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int SupplierId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int TaxId { get; set; }
        public string UPC { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int AgeLimit { get; set; }
        public int Quantity { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Margin { get; set; }
        public bool HaveDiscount { get; set; }
        public DateTimeOffset? ExpiredDate { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public byte[] Timestamp { get; set; } = null!;

        public static Product Exec(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/product/get/{id}");

            return client.ExecuteWithHttp<Product>(request);
        }

        public static Product ExecTest(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/product/get/{id}");

            return client.ExecuteWithHttp<Product>(request);
        }
    }
}