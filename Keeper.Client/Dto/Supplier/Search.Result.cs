using System.Collections.Generic;

namespace Keeper.Client
{
    public partial class Supplier
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
                    public string CompanyName { get; set; } = null!;
                    public string Phone { get; set; } = null!;
                    public string? Email { get; set; }
                    public string? Address { get; set; }
                }
            }
        }
    }
}