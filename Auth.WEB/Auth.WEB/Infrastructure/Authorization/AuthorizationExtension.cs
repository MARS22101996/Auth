using Microsoft.AspNetCore.Builder;

namespace Auth.WEB.Infrastructure.Authorization
{
    public static class AuthorizationExtension
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
			//return builder.UseMiddleware<AuthorizationMiddleware>();
			return builder.UseMiddleware<AuthorizationMiddleware>();
		}
    }
}
