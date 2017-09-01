using System;
using System.Security.Claims;

namespace AuthClient.Infrastructure.Authorization
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirst("userId").Value);
        }

        public static string GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
        }
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}