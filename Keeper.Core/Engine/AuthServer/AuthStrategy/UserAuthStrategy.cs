using Keeper.Client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Newtonsoft.Json;

namespace Keeper.Core
{
    public class UserAuthStrategy
    {
        UserEngine m_users;
        TimeSpan m_tokenExpired;
        IDataProtector m_protector;
        JsonSerializer m_serializer;

        SecureDataFormat_Owin<AuthenticationTicket_Owin> m_security;

        public UserAuthStrategy(SecureDataFormat_Owin<AuthenticationTicket_Owin> security, UserEngine users,
            TimeSpan tokenExpired, TimeSpan sessionExpired,
               IDataProtector protector, JsonSerializer serializer)
        {
            m_security = security;

            m_users = users;
            m_tokenExpired = tokenExpired;
            m_protector = protector;
            m_serializer = serializer;
        }

        public TokenInfo GetToken(RequestStruct request)
        {
            var elementsFromForm = request.Items;

            var userInfo = m_users.AuthCheckUser(request.GetValue("username"), request.GetValue("password"));

            var result = BuildToken(userInfo);

            return result;
        }

        private TokenInfo BuildToken(UserInfoExtension userInfo)
        {
            var result = new TokenInfo();
            var reader = new UserIdentityReader(Guid.NewGuid());
            reader.SessionExpireTime = DateTime.Now.AddDays(7);

            var identity = userInfo.UserToIdentity();

            var expiredTime = DateTime.UtcNow + m_tokenExpired;
            identity.InitTicket(reader.SessionId, expiredTime, reader.SessionExpireTime);

            reader.UserId = userInfo.UserId;
            reader.PasswordHash = userInfo.PasswordHash;

            result.TokenType = "BEARER";
            result.AccessToken = m_security.Protect(identity);
            result.RefreshToken = reader.PrepareRefreshToken(m_protector, m_serializer);
            result.ExpireTime = (int)(expiredTime - DateTime.UtcNow).TotalSeconds;
            return result;
        }
    }
}
