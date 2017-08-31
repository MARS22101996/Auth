using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.BLL.Infrastructure.CryptoProviders;
using UserService.BLL.Infrastructure.DI;
using UserService.BLL.Interfaces;
using UserService.BLL.Services;
using UserService.WEB.Authentication;
using UserService.WEB.Authentication.Interfaces;

namespace UserService.WEB.Infrastructure.DI
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