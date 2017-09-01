using AuthHost.BLL.Infrastructure.CryptoProviders;
using AuthHost.BLL.Infrastructure.DI;
using AuthHost.BLL.Interfaces;
using AuthHost.BLL.Services;
using AuthHost.WEB.Authentication;
using AuthHost.WEB.Authentication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthHost.WEB.Infrastructure.DI
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICryptoProvider, MD5CryptoProvider>();
            services.AddTransient<IIdentityProvider, IdentityProvider>();

            DependencyResolverModule.Configure(services, configuration);
        }
    }
}