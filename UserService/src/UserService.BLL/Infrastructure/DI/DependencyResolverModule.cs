using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UserService.DAL.Context;
using UserService.DAL.Interfaces;
using UserService.DAL.UnitOfWorks;

namespace UserService.BLL.Infrastructure.DI
{
    public class DependencyResolverModule
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration["ConnectionStrings:MongoDb"];

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbContext>(provider => new DbContext(connectionstring));
        }
    }
}