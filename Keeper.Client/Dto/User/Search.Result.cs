using System.Collections.Generic;


namespace Keeper.Client
{
    public partial class User
    {
        public partial class Search
        {
             public class Result
             {
                 public List<Item> Items { get; set; } = new List<Item>();
                 public long Total { get; set; }

                 public class Item
                 {
					public int Id { get; set;} 
					public string FullName  { get; set;} = null!; 
					public string Phone  { get; set;} = null!;
                    public string? Email { get; set; }
					public string Login  { get; set;} = null!;
                    public Status Status { get; set; }
                    public UserType UserType { get; set; }
                }
            }
        }
    }
}