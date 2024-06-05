using Row.Common1.Client1;

namespace Keeper.Client
{
    public class KeeperApiClient : ApiClient
    {
        public KeeperApiClient(string baseUrl)
            : base(baseUrl) // + "/api/v1")
        {
            
        }
    }
}
