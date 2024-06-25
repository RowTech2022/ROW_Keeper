using Row.Common.Dto1;

namespace Keeper.Client;

public partial class Subscription
{
    public partial class Search
    {
        public int[]? Ids { get; set; }
        public Filter Filters { get; set; } = new Filter();
        public PageInfo PageInfo { get; set; } = new PageInfo();
    }
}