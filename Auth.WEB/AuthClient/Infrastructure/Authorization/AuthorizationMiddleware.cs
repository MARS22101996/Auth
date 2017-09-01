using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuthClient.Infrastructure.Authorization
{
    public class AuthorizationMiddleware
    {
        protected const string CookieTokenKeyName = "token";

        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            context.Request.Headers["Authorization"] = $"Bearer {context.Request.Cookies[CookieTokenKeyName]}";


            await _next.Invoke(context);
        }
    }
}
