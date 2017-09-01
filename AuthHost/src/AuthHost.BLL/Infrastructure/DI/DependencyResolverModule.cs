using AuthHost.DAL.Context;
using AuthHost.DAL.Interfaces;
using AuthHost.DAL.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthHost.BLL.Infrastructure.DI
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