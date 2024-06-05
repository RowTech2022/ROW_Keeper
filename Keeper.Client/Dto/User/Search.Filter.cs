using Keeper.Client.Validation;
using Row.Common.Dto1;


namespace Keeper.Client
{
    public partial class User
    {
        public partial class Search
        {
            public class Filter
            {
                [TrimWhitespace]
                public string? Login { get; set; }
                
                [TrimWhitespace]
                public string? Email { get; set; }
            }

            public class OrderByInfo
            {
                public SearchByOrder? OrderColumn { get; set; }
                public SearchByDirection Direction { get; set; }

                public OrderByInfo() { }

                public OrderByInfo(SearchByOrder? column, SearchByDirection order)
                {
                    OrderColumn = column;
                    Direction = order;
                }
            }

            public enum SearchByOrder
            {
                Id = 1
            }
        }
    }
}