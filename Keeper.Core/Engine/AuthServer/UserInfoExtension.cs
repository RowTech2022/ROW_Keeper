using Row.Common1;

namespace Keeper.Core
{
    public class UserInfoExtension : UserInfo
    {
        public int BranchId { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
    }
}
