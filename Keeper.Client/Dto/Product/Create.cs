using System;
using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client.Product
{
    public partial class Product
    {
        public class Create
        {
            public int SupplierId { get; set; }

            public int CategoryId { get; set; }

            public int TaxId { get; set; }

            [Required]
            [TrimWhitespace(100)]
            public string UPC { get; set; } = null!;
    
            [Required]
            [TrimWhitespace(500)]
            public string Name { get; set; } = null!;

            public int AgeLimit { get; set; }

            public int Quantity { get; set; }

            public decimal BuyingPrice { get; set; }

            public decimal Price { get; set; }

            public decimal TotalPrice { get; set; }

            public int Margin { get; set; }

            public DateTimeOffset? ExpiredDate { get; set; }

            public Product Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/product/create").Body(this);

                return client.ExecuteWithHttp<Product>(request);
            }

            public Product ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/product/create").Body(this);

                return client.ExecuteWithHttp<Product>(request);
            }
        }
    }
}