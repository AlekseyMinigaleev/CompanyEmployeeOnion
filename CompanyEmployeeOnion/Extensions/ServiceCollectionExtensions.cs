using Database;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;

namespace CompanyEmployeeOnion.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddControllersConfiguration(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(typeof(CompanyController).Assembly);

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public static void AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(builder =>
            {
                var connectionString = configuration
                    .GetConnectionString("DefaultConnection");

                builder.UseNpgsql(connectionString);
            });
        }
    }
}
