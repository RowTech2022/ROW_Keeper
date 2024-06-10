using System.Collections.Generic;

namespace Keeper.Client
{
    public partial class Category
    {
        public partial class Search
        {
            public class Result
            {
                public List<Item> Items { get; set; } = new List<Item>();
                public int Total { get; set; }
                
                public class Item
                {
                    public int Id { get; set; }
                    public List<Item>? SubCategories { get; set; } 
                    public string Name { get; set; } = null!;
                    public string? Description { get; set; }
                }
            }
        }
    }
}