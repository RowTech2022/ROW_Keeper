using System;

namespace Keeper.Client.ProductDiscount
{
    public partial class ProductDiscount
    {
        public class Create
        {
            public int ProductId { get; set; }
            public double Percent { get; set; }
            public string? Comment { get; set; }
            public DateTimeOffset FromDate { get; set; }
            public DateTimeOffset ToDate { get; set; }
        }
    }
}