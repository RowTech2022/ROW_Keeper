namespace Keeper.Client
{
    public partial class Organization : BaseDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string OrgName { get; set; } = null!;
        public string OrgPhone { get; set; } = null!;
        public string OrgAddress { get; set; } = null!;
    }
}