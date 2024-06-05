namespace Keeper.Core
{
    public class ClientIdRepository
    {
        const string c_prefix = "ClientId.";
        const string c_resourceSuffix = ".Resources";

        Dictionary<string, Settings> m_clients = new Dictionary<string, Settings>();
        TimeSpan m_defaultExpirTime;

        public ClientIdRepository(TimeSpan defaultServiceTokenExpiration)
        {
            m_defaultExpirTime = defaultServiceTokenExpiration;
        }

        public ClientIdRepository LoadData()
        {
            return this;
        }

        public class Settings
        {
            public string? ServiseName { get; set; }
            public string? ServiseKey { get; set; }
            public HashSet<string> Roles { get; set; }
        }

    }
}
