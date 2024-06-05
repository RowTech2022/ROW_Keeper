using Keeper.Client.Validation;

namespace Keeper.Client
{
    public partial class OrganizationBranch
    {
        public class Create
        {
            public int OwnerId { get; set; }

            [TrimWhitespace(500)]
            public string BranchName { get; set; } = null!;

            [TrimWhitespace(20)]
            public string BranchPhone { get; set; } = null!;

            [TrimWhitespace(500)]
            public string BranchAddress { get; set; } = null!;
        }
    }
}