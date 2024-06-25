using System.ComponentModel.DataAnnotations;
using Keeper.Client.Validation;
using Row.Common1.Client1;

namespace Keeper.Client;

public partial class Plan
{
    public class Create
    {
        [TrimWhitespace(500)]
        public string Name { get; set; } = null!;
        
        [Range(0, 9999999)]
        public decimal Price { get; set; }

        [Range(0, 9999999)]
        public int Duration { get; set; }

        public PlanType Type { get; set; }

        public Plan Exec(KeeperApiClient client)
        {
            var request = client.PostRequest("api/plan/create").Body(this);

            return client.ExecuteWithHttp<Plan>(request);
        }

        public Plan ExecTest(KeeperApiClient client)
        {
            var request = client.PostRequest("api/plan/create").Body(this);

            return client.ExecuteWithHttp<Plan>(request);
        }
    }
}