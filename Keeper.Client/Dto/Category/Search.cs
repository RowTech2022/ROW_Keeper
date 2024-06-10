using Row.Common.Dto1;

namespace Keeper.Client
{
    public partial class Category
    {
        public partial class Search
        {
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();
        }
    }
}