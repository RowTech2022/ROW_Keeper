using System.Collections.Generic;

namespace Keeper.Client
{
    public partial class OrganizationBranch
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
                    public string BranchName  { get; set;} = null!;
                    public string BranchPhone { get; set;} = null!;
                    public string? BranchEmail { get; set;}
                    public string BranchAddress { get; set; } = null!;
                }
            }
        }
    }
}