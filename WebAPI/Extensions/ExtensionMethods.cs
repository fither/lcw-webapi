using DataAccess.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using WebAPI.LoggerService;

namespace WebAPI.Extensions
{
    public static class ExtensionMethods
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(ConnString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
        }
    }
}
