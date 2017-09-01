using Microsoft.AspNetCore.Builder;

namespace AuthClient.Infrastructure.Authorization
{
    public static class AuthorizationExtension
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}