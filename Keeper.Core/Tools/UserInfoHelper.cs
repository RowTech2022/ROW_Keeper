using Microsoft.AspNetCore.Http;
using Row.Common1;

namespace Keeper.Core;

public static class UserInfoHelper
{
    public static UserInfoExtension GetUserInfoHelper(this RequestInfo requestInfo, HttpContext httpContext, IHeaderDictionary headers = null, bool checkCert = false)
    {
        var userInfo = requestInfo.GetUserInfo(httpContext, headers, checkCert);

        var result = new UserInfoExtension
        {
            UserId = userInfo.UserId,
            OrganisationId = Convert.ToInt32(httpContext.User.Claims.FirstOrDefault(x => x.Type == "OrganizationId")?.Value),
            SessionId = userInfo.SessionId,
            Expired = userInfo.Expired,
            Roles = userInfo.Roles,
            Type = userInfo.Type,
            Avatar = userInfo.Avatar,
            Name = userInfo.Name,
            SertId = userInfo.SertId
        };

        return result;
    }
}