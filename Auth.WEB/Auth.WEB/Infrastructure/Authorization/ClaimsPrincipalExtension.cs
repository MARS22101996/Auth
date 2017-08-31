using System;
using System.Security.Claims;

namespace Auth.WEB.Infrastructure.Authorization
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirst("userId").Value);
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
			var i = claimsPrincipal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

			var test = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

			return claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}