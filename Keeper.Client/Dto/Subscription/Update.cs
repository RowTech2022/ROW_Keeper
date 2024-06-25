using System;
using Row.Common1.Client1;

namespace Keeper.Client;

public partial class Subscription
{
    public class Update
    {
        public int Id { get; set; }
            
        public DateTimeOffset StartDate { get; set; }
            
        public DateTimeOffset EndDate { get; set; }

        public Subscription Exec(KeeperApiClient client)
        {
            var request = client.PostRequest("api/subscription/update").Body(this);

            return client.ExecuteWithHttp<Subscription>(request);
        }

        public Subscription ExecTest(KeeperApiClient client)
        {
            var request = client.PostRequest("api/subscription/update").Body(this);

            return client.ExecuteWithHttp<Subscription>(request);
        }
    }
}