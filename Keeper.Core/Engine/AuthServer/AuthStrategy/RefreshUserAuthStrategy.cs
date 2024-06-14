using Keeper.Client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Newtonsoft.Json;

namespace Keeper.Core
{
    public class RefreshUserAuthStrategy
    {
        UserEngine m_users;
        TimeSpan m_tokenExpired;
        IDataProtector m_protector;
        JsonSerializer m_serializer;

        SecureDataFormat_Owin<AuthenticationTicket_Owin> m_security;

        public RefreshUserAuthStrategy(SecureDataFormat_Owin<AuthenticationTicket_Owin> security, UserEngine users, TimeSpan tokenExpired, IDataProtector protector, JsonSerializer serializer)
        {
            m_users = users;
            m_security = security;

            m_tokenExpired = tokenExpired;
            m_protector = protector;
            m_serializer = serializer;
        }

        public TokenInfo GetToken(RequestStruct request)
        {
            var elementsFromForm = request.Items;

            var result = new TokenInfo();

            var reader = UserIdentityReader.FromRefreshToken(request.GetValue("refresh_token"), m_protector, m_serializer);

            reader.AssertExpired();

            var userInfo = m_users.RefreshCheckUser(reader.UserId, reader.PasswordHash);

            var ticket = userInfo.UserToIdentity();

            var expiredTime = GetExpireTime(reader);

            ticket.InitTicket(reader.SessionId, expiredTime, reader.SessionExpireTime, userInfo);

            result.TokenType = "Bearer";
            result.AccessToken = m_security.Protect(ticket);
            result.RefreshToken = reader.PrepareRefreshToken(m_protector, m_serializer);
            result.ExpireTime = (int)(expiredTime - DateTime.UtcNow).TotalSeconds;

            return result;
        }

        private DateTime GetExpireTime(UserIdentityReader reader)
        {
            var expireTime = DateTime.UtcNow + m_tokenExpired;
            if (expireTime > reader.SessionExpireTime)
                expireTime = reader.SessionExpireTime;

            return expireTime;
        }
    }
}
