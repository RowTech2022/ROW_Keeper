using System;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Category
    {
        public int Id { get; set; }
                
        public int OrgId { get; set; }
                
        public int ParentId { get; set; }
    
        public string Name { get; set; } = null!;
    
        public string? Description { get; set; }
        
        public bool Active { get; set; }
    
        public DateTimeOffset CreatedAt { get; set; }
    
        public DateTimeOffset UpdatedAt { get; set; }

        public byte[] Timestamp { get; set; } = null!;
        

        public Category Exec(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/category/get/{id}").Body(this);

            return client.ExecuteWithHttp<Category>(request);
        }

        public Category ExecTest(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/category/get/{id}").Body(this);

            return client.ExecuteWithHttp<Category>(request);
        }
    }
}