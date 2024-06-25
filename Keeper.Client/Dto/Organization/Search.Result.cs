using System.Collections.Generic;

namespace Keeper.Client
{
    public partial class Organization
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
                    public Information? Plan { get; set; }
                    public string OrgName  { get; set;} = null!;
                    public string OrgPhone { get; set;} = null!;
                    public string? OrgEmail { get; set;} = null!;
                    public string OrgAddress { get; set; } = null!;
                    public OrgStatus Status { get; set; }
                }
            }
        }
    }
}