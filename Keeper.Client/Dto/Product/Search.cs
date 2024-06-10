using Row.Common.Dto1;

namespace Keeper.Client.Product
{
    public partial class Product
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
        }
    }
}