using Microsoft.Extensions.DependencyInjection;
using Services.Company;
using Services.Employee;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
