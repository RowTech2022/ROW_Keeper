using Row.Common.Dto1;

namespace Keeper.Client.ProductDiscount
{
    public partial class ProductDiscount
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
        }
    }
}