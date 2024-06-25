using System;
using System.Collections.Generic;

namespace Keeper.Client;

public partial class Subscription
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

                public Information Organization { get; set; } = null!;

                public Information Plan { get; set; } = null!;
            
                public DateTimeOffset StartDate { get; set; }
            
                public DateTimeOffset EndDate { get; set; }
            }
        }
    }
}