using System.ComponentModel.DataAnnotations;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Category
    {
        public class Update
        {
            public int Id { get; set; }
            
            public int ParentId { get; set; }
    
            [StringLength(300)]
            public string Name { get; set; } = null!;
    
            [StringLength(3000)]
            public string? Description { get; set; }
        

            public Category Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/update").Body(this);

                return client.ExecuteWithHttp<Category>(request);
            }

            public Category ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/update").Body(this);

                return client.ExecuteWithHttp<Category>(request);
            }
        }
    }
}