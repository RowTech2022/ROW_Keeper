using Microsoft.AspNetCore.Http;
using System.Text;

namespace Keeper.Core
{
    public class RequestStruct
    {
        public IFormCollection Items { get; set; }
        public Dictionary<string, string> ItemsObsolete { get; set; } = new Dictionary<string, string>();

        bool oldLogic;

        public RequestStruct(bool old = false)
        {
            oldLogic = old;
        }

        public RequestStruct LoadFromRequestBody(HttpRequest request)
        {
            var formData = request.Form;
            Items = formData;

            return this;
        }

        public RequestStruct LoadFromDto(Client.Login request)
        {
            // ItemsObsolete["grant_type"] = request.GrantType;
            ItemsObsolete["username"] = request.UserLogin;
            ItemsObsolete["password"] = request.Password;

            return this;
        }

        public bool IsServiceCode(string code)
        {
            if (Items.ContainsKey("grant_type") && Items.ContainsKey("code"))
                return (StringComparer.InvariantCultureIgnoreCase.Equals(Items["grant_type"].ToString()?.Trim(), "authorization_code")
                        && StringComparer.InvariantCultureIgnoreCase.Equals(Items["code"].ToString()?.Trim(), code));
            return false;
        }

        public bool IsUserAuth()
        {
            if (oldLogic)
                return IsUserAuthObsolete();

            if (Items.ContainsKey("grant_type"))
                return StringComparer.InvariantCultureIgnoreCase.Equals(Items["grant_type"].ToString()?.Trim(), "password");
            return false;
        }

        public bool IsUserAuthObsolete()
        {
            if (ItemsObsolete.ContainsKey("grant_type"))
                return StringComparer.InvariantCultureIgnoreCase.Equals(ItemsObsolete["grant_type"].ToString()?.Trim(), "password");
            return false;
        }

        public bool IsRefreshToken()
        {
            if (oldLogic)
                return IsRefreshTokenObsolete();

            if (Items.ContainsKey("grant_type"))
                return StringComparer.InvariantCultureIgnoreCase.Equals(Items["grant_type"].ToString()?.Trim(), "refresh_token");
            return false;
        }

        public bool IsRefreshTokenObsolete()
        {
            if (ItemsObsolete.ContainsKey("grant_type"))
                return StringComparer.InvariantCultureIgnoreCase.Equals(ItemsObsolete["grant_type"].ToString()?.Trim(), "refresh_token");
            return false;
        }

        public string GetValue(string key)
        {
            if (oldLogic)
            {
                if (ItemsObsolete.ContainsKey(key))
                    return System.Web.HttpUtility.UrlDecode(ItemsObsolete[key], Encoding.UTF8);
                else
                    return "";
            }
            else
            {
                if (Items.ContainsKey(key))
                    return System.Web.HttpUtility.UrlDecode(Items[key], Encoding.UTF8);
                else
                    return "";
            }
        }
    }
}
