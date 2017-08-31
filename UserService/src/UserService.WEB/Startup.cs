using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using UserService.WEB.Authentication;
using UserService.WEB.Authentication.Middlewares;
using UserService.WEB.Filters;
using UserService.WEB.Infrastructure.DI;

namespace UserService.WEB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            env.ConfigureNLog("NLog.config");
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DependencyResolver.Resolve(services, Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .Configure<TokenProviderOptions>(Configuration.GetSection("TokenProviderOptions"))
                .Configure<TokenProviderOptions>(options =>
                {
                    var signingKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(options.SecretKey));
                    options.SigningCredentials =
                        new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                });

            services
                .Configure<TokenValidationParameters>(Configuration.GetSection("TokenValidationParameters"))
                .Configure<TokenValidationParameters>(options =>
                {
                    var signingKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["TokenValidationParameters:SecretKey"]));
                    options.IssuerSigningKey = signingKey;
                });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ErrorFilter));
            });

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "User Service API" });
                c.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IOptions<TokenProviderOptions> tokenProviderOptions,
            IOptions<TokenValidationParameters> tokenValidationOptions)
        {
            loggerFactory.AddNLog();
            app.AddNLogWeb();



            ConfigLogManager();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMiddleware<TokenProviderMiddleware>(tokenProviderOptions);

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationOptions.Value
            });

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

        }

        private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, $"{appEnvironment.ApplicationName}.xml");
        }

        private void ConfigLogManager()
        {
            LogManager.Configuration.Variables["configDir"] = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        }
    }
}