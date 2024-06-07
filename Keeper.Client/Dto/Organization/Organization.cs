namespace Keeper.Client
{
    public partial class Organization : BaseDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string OrgName { get; set; } = null!;
        public string OrgPhone { get; set; } = null!;
        public string? OrgEmail { get; set; } = null!;
        public string OrgAddress { get; set; } = null!;
        
        public static Organization Exec(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organization/get/{id}");

            return client.ExecuteWithHttp<Organization>(request);
        }

        public static Organization ExecTest(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organization/get/{id}");

            return client.ExecuteWithHttp<Organization>(request);
        }
    }
}