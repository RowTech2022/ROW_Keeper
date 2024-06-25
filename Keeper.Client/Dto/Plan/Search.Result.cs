using System.Collections.Generic;

namespace Keeper.Client;

public partial class Plan
{
    public partial class Search
    {
        public class Result
        {
            public List<Item> Items { get; set; } = [];
            public int Total { get; set; }
            
            public class Item
            {
                public int Id { get; set; }
        
                public string Name { get; set; } = null!;
        
                public decimal Price { get; set; }

                public int Duration { get; set; }

                public PlanType Type { get; set; }
            }
        }
    }
}