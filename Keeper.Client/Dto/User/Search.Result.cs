using System;
using System.Collections.Generic;
using Row.Common.Dto1;


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
					public string Name  { get; set;} = null!; 
					public string Surname  { get; set;} = null!; 
					public string Phone  { get; set;} = null!; 
					public string Login  { get; set;} = null!;
                    public Status State { get; set; }
                    public UserType UserType { get; set; }
                }
            }
        }
    }
}