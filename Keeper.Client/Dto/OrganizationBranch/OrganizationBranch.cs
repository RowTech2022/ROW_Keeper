namespace Keeper.Client
{
    public partial class OrganizationBranch : BaseDto
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string BranchName { get; set; } = null!;
        public string BranchPhone { get; set; } = null!;
        public string BranchAddress { get; set; } = null!;
    }
}