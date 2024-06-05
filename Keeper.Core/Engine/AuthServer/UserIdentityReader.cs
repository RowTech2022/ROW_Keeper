using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Row.Common1;
using Row.Common1.Client1;
using System.Security.Claims;

namespace Keeper.Core
{
    public class UserIdentityReader
    {
        public const string c_authType = "Bearer";

        public int UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public string AuthType { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime SessionExpireTime { get; set; }
        public Guid SessionId { get; set; }


        public UserIdentityReader()
        {
            AuthType = c_authType;
            IssuedTime = DateTime.UtcNow;
        }

        public UserIdentityReader(Guid sessionId) : this()
        {
            SessionId = sessionId;
        }

        public UserIdentityReader(Guid sessionId, int userId, byte[] passwordHash) : this()
        {
            SessionId = sessionId;
            UserId = userId;
            PasswordHash = passwordHash;
        }

        public void AssertExpired()
        {
            if (SessionExpireTime.ToUniversalTime() < DateTime.UtcNow)
                throw new UnauthorizedApiException($"Access denied.");
        }

        public static UserIdentityReader FromRefreshToken(string data, IDataProtector protector, JsonSerializer serializer)
        {
            var protect = HttpServerUtility.UrlTokenDecode(data);

            var bData = protector.Unprotect(protect);

            using (var memory = new MemoryStream(bData))
            using (var reader = new BsonDataReader(memory))
                return serializer.Deserialize<UserIdentityReader>(reader);
        }

        public string PrepareRefreshToken(IDataProtector protector, JsonSerializer serializer)
        {
            using (var memory = new MemoryStream())
            using (var writer = new BsonDataWriter(memory))
            {
                serializer.Serialize(writer, this);
                writer.Flush();

                var protect = protector.Protect(memory.ToArray());
                return HttpServerUtility.UrlTokenEncode(protect);
            }
        }
    }

    public static class AuthenticationTicketHelper
    {
        /// <summary>
        /// clientId value is required for the [AuthorizationCodeProvider_OnReceive method] only
        /// Without the value system will return very strange exception. Please pay attention on the comment.
        /// </summary>
        public static AuthenticationTicket_Owin InitTicket(this AuthenticationTicket_Owin ticket, Guid sessionId, DateTime expireTime, DateTime sessionExpireTime)
        {
            ticket.Properties.IsPersistent = false;
            ticket.Properties.ExpiresUtc = expireTime;
            ticket.Properties.IssuedUtc = DateTime.UtcNow;

            ticket.Identity.AddClaims(new List<Claim>() { UserInfo.CreateSessionIdClaim(sessionId) });
            ticket.Identity.AddClaims(new List<Claim>() { UserInfo.CreateExpiredClaim(sessionExpireTime) });

            return ticket;
        }

        public static AuthenticationTicket_Owin UserToIdentity(this UserInfoExtension userInfo)
        {
            var identity = new ClaimsIdentity(UserIdentityReader.c_authType);
            identity.AddClaims(userInfo.ToClaimList());

            return new AuthenticationTicket_Owin(identity, new AuthenticationProperties_Owin());
        }
    }
}
