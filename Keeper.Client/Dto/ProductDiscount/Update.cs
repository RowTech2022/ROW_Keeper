using System;

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
        }
    }
}