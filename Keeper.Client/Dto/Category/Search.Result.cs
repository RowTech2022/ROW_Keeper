using System.Collections.Generic;

namespace Keeper.Client
{
    public partial class Category
    {
        public partial class Search
        {
            public class CategoryResult
            {
                public List<Item> Items { get; set; } = new List<Item>();
                public int Total { get; set; }
                
                public class Item
                {
                    public int Id { get; set; }
                    public string Name { get; set; } = null!;
                }
            }
            
            public class SubCategoryResult
            {
                public List<Item> Items { get; set; } = new List<Item>();
                public int Total { get; set; }
                
                public class Item
                {
                    public int Id { get; set; }
                    public string CategoryName { get; set; } = null!;
                    public string SubCategoryName { get; set; } = null!;
                }
            }
        }
    }
}