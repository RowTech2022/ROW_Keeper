using Bibliotekaen.Sql;
using Keeper.Client;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Row.Common1.Client1;

namespace Keeper.Core
{
    public partial class AuthEngine
	{
		UserEngine m_userEngine;

		AuthServerSettings m_authServerSettings;
        HashCalculator m_hashCalculator = new HashCalculator("#####");

        LanguageService m_languageServices;

		ISendStrategy m_smsStrategy;

		ISqlFactory m_sql;

		UserAuthStrategy m_userStrategy;

		RefreshUserAuthStrategy m_regreshStrategy;

		public AuthEngine(UserEngine userEngine, AuthServerSettings settings, ISqlFactory sql, ISendStrategy sendStrategy)
		{
			m_userEngine = userEngine;
			m_authServerSettings = settings;
			m_sql = sql;
			m_smsStrategy = sendStrategy;

			var security = new SecureDataFormat_Owin<AuthenticationTicket_Owin>(new TicketSerializer_Owin(), m_authServerSettings.AuthProtector);

			var refreshTokenProtector = m_authServerSettings.TokenProtector.CreateProtector("RefreshTokenProvider");

			m_userStrategy = new UserAuthStrategy(security, m_userEngine, m_authServerSettings.ExpireTime, m_authServerSettings.RefreshExpireTime,
				refreshTokenProtector, m_authServerSettings.Serializer);

			m_regreshStrategy = new RefreshUserAuthStrategy(security, m_userEngine, m_authServerSettings.ExpireTime, refreshTokenProtector,
				m_authServerSettings.Serializer);
		}

		public void SetSmsStrategy(ISendStrategy sendStrategy)
		{
			m_smsStrategy = sendStrategy;
		}

		public AuthEngine (ISendStrategy sendStrategy)
		{
            m_smsStrategy = sendStrategy;
        }

        public AuthEngine InitLanguageServise(LanguageService languageService)
		{
			m_userEngine = m_userEngine.InitLanguageServise(languageService);
			m_languageServices = languageService;
			return this;
		}

		public TokenInfo Login(Login request)
		{
			return m_userStrategy.GetToken(request);
		}

		public TokenInfo Refresh(Refresh request)
		{
			var security = new SecureDataFormat_Owin<AuthenticationTicket_Owin>(new TicketSerializer_Owin(), m_authServerSettings.AuthProtector);

			var refreshTokenProtector = m_authServerSettings.TokenProtector.CreateProtector("RefreshTokenProvider");

			var code = new RefreshUserAuthStrategy(security, m_userEngine, m_authServerSettings.ExpireTime, refreshTokenProtector,
				m_authServerSettings.Serializer);
			var bodyElement = new RequestStruct(true);
			bodyElement.ItemsObsolete["refresh_token"] = request.RefreshToken;

			return code.GetToken(bodyElement);
		}
	}
}