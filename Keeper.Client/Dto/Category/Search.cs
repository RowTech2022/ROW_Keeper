using Row.Common.Dto1;
using Row.Common1.Client1;

namespace Keeper.Client
{
    public partial class Category
    {
        public partial class Search
        {
            public int[]? Ids { get; set; }
            public Filter Filters { get; set; } = new Filter();
            public PageInfo PageInfo { get; set; } = new PageInfo();

            public CategoryResult ExecCategory(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/searchCategory").Body(this);

                return client.ExecuteWithHttp<CategoryResult>(request);
            }

            public SubCategoryResult ExecSubCategory(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/searchSubCategory").Body(this);

                return client.ExecuteWithHttp<SubCategoryResult>(request);
            }

            public CategoryResult ExecCategoryTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/searchCategory").Body(this);

                return client.ExecuteWithHttp<CategoryResult>(request);
            }

            public SubCategoryResult ExecSubCategoryTest(KeeperApiClient client)
            {
                var request = client.PostRequest("api/category/searchSubCategory").Body(this);

                return client.ExecuteWithHttp<SubCategoryResult>(request);
            }
        }
    }
}