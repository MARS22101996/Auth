using Auth.WEB.RequestSettings;
using Auth.WEB.RequestSettings.Inerfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.WEB.Infrastructure.DI
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRequestService, RequestService>();
        }
    }
}
