using Keeper.Client;

namespace Keeper.Core
{
    public interface ISendStrategy
	{
		void SendCodeToPhone(User.UserCode request, string phone, string Code, string UserName);
		void SendLoginAndPasswordToPhone(string phone, string login, string pass);
    }
}