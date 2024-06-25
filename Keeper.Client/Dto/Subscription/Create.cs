using Row.Common1.Client1;

namespace Keeper.Client;

public partial class Subscription
{
    public class Create
    {
        public int OrgId { get; set; }
            
        public int PlanId { get; set; }

        public Subscription Exec(KeeperApiClient client)
        {
            var request = client.PostRequest("api/subscription/create").Body(this);

            return client.ExecuteWithHttp<Subscription>(request);
        }

        public Subscription ExecTest(KeeperApiClient client)
        {
            var request = client.PostRequest("api/subscription/create").Body(this);

            return client.ExecuteWithHttp<Subscription>(request);
        }
    }
}