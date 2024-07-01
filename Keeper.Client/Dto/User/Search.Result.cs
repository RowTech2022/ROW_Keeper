using System.Collections.Generic;


namespace Keeper.Client
{
    public partial class User
    {
        public partial class Search
        {
             public class Result
             {
                 public List<Item> Items { get; set; } = [];
                 public long Total { get; set; }

                 public class Item
                 {
					public int Id { get; set;} 
					public string FullName  { get; set;} = null!; 
					public string Phone  { get; set;} = null!;
                    public string OrgName { get; set; } = null!;
					public string Login  { get; set;} = null!;
                    public Status State { get; set; }
                    public UserType UserType { get; set; }
                    public string RoleName { get; set; } = null!;
                 }
            }
        }
    }
}