using System.ComponentModel.DataAnnotations;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Category
    {
        public class Create
        {
            public int? ParentId { get; set; }
    
            [StringLength(300)]
            public string Name { get; set; } = null!;

            public Category Exec(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/create").Body(this);

                return client.ExecuteWithHttp<Category>(request);
            }

            public Category ExecTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/create").Body(this);

                return client.ExecuteWithHttp<Category>(request);
            }
        }
    }
}