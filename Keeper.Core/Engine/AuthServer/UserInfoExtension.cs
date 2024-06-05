using Row.Common1;

namespace Keeper.Core
{
    public class UserInfoExtension : UserInfo
    {
        public byte[]? PasswordHash { get; set; }
    }
}
