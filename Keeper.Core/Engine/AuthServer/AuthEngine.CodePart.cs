using Row.Common1.Client1;
using System.Text.RegularExpressions;
using Keeper.Client;

namespace Keeper.Core
{
    public partial class AuthEngine
	{
		public void SendCode(User.UserCode request)
		{
			var db = m_userEngine.SendCodeCheckUser(request.Login, request.Login);
			var phone = "992123456789";
            Regex regex = new Regex(@"(992([0-9]{9}))");
            Match match = regex.Match(phone);
            if (!match.Success)
                throw new RecordNotFoundApiException("Не правильный формат телефона");

            var code = SaveCode(request.Login);
            if (code <= 0)
                throw new RecordNotFoundApiException("Код должен быть меньше нуля");

            m_smsStrategy.SendCodeToPhone(request, phone, code.ToString(), request.Login);
        }

        public int SaveCode(string login)
		{
			Random rnd = new Random();
			int value = rnd.Next(1000, 9999);
			new Db.UserAuthCode.Create()
			{
				PhoneNumber = login,
				Active = true,
				Code = value
			}.Exec(m_sql);

			return value;
		}
	}
}