using System.Collections.Generic;

namespace Keeper.Client;

public partial class User
{
    public partial class Role
    {
        public class List
        {
            public List<Item> Items { get; set; } = [];
            
            public class Item
            {
                public int RoleId { get; set; }
                public string RoleName { get; set; } = null!;
            }
        }
    }
}