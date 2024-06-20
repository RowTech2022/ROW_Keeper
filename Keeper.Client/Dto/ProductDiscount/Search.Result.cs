using System;
using System.Collections.Generic;

namespace Keeper.Client.ProductDiscount
{
    public partial class ProductDiscount
    {
        public partial class Search
        {
            public class Result
            {
                public List<Item> Items { get; set; } = new List<Item>();
                
                public class Item : BaseDto
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
                }
            }
        }
    }
}