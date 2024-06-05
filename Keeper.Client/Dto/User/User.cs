using System;

namespace Keeper.Client
{
    public partial class User
    {
		public int Id  { get; set;}
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string? Email  { get; set;}
		public string Phone  { get; set;} = null!;
		public string Login { get; set; } = null!;
		public Status State { get; set; }
        public UserType UserType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public byte[] Timestamp { get; set; } = null!;

         public static User Exec(int id, KeeperApiClient client)
         {
             var req = client.GetRequest($"api/User/get/{id}");

             return client.Execute<User>(req);
         }

         public static User ExecTest(int id, KeeperApiClient client)
         {
             var req = client.GetRequest($"api/User/get/{id}");

             return client.ExecuteWithHttp<User>(req);
         }
    }
}