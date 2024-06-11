using System;
using System.Collections.Generic;

namespace Keeper.Client.Product
{
    public partial class Product
    {
        public partial class Search
        {
            public class Result
            {
                public List<Item> Items { get; set; } = new List<Item>();
                public int Total { get; set; }
            
                public class Item
                {
                    public int Id { get; set; }
                    public string CategoryName { get; set; } = null!;
                    public string UPC { get; set; } = null!;
                    public string Name { get; set; } = null!;
                    public int AgeLimit { get; set; }
                    public int Quantity { get; set; }
                    public decimal Price { get; set; }
                    public decimal DiscountPrice { get; set; }
                    public bool HaveDiscount { get; set; }
                    public DateTimeOffset? ExpiredDate { get; set; }
                }
            }
        }
    }
}