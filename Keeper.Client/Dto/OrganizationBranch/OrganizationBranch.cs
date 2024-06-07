namespace Keeper.Client
{
    public partial class OrganizationBranch : BaseDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string BranchName { get; set; } = null!;
        public string BranchPhone { get; set; } = null!;
        public string BranchAddress { get; set; } = null!;
        
        public static Organization Exec(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organizationBranch/get/{id}");

            return client.ExecuteWithHttp<Organization>(request);
        }

        public static Organization ExecTest(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organizationBranch/get/{id}");

            return client.ExecuteWithHttp<Organization>(request);
        }
    }
}