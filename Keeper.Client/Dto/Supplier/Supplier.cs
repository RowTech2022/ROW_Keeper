namespace Keeper.Client
{
    public partial class Supplier : BaseDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }

        public static Supplier Exec(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/supplier/get/{id}");

            return client.ExecuteWithHttp<Supplier>(request);
        }

        public static Supplier ExecTest(int id, KeeperApiClient client)
        {
            var request = client.GetRequest($"api/supplier/get/{id}");

            return client.ExecuteWithHttp<Supplier>(request);
        }
    }
}