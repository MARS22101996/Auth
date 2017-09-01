using AuthClient.RequestSettings;
using AuthClient.RequestSettings.Inerfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthClient.Infrastructure.DI
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRequestService, RequestService>();
        }
    }
}
