namespace Keeper.Client
{
    public partial class OrganizationBranch : BaseDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string BranchName { get; set; } = null!;
        public string BranchPhone { get; set; } = null!;
        public string? BranchEmail { get; set; }
        public string BranchAddress { get; set; } = null!;
        
        public static OrganizationBranch Exec(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organizationBranch/get/{id}");

            return client.ExecuteWithHttp<OrganizationBranch>(request);
        }

        public static OrganizationBranch ExecTest(int id, KeeperApiClient client)
        {
            var request = client.PostRequest($"api/organizationBranch/get/{id}");

            return client.ExecuteWithHttp<OrganizationBranch>(request);
        }
    }
}