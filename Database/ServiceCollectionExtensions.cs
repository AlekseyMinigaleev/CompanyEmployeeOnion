using Database.Company;
using Database.Employee;
using Domain.Company;
using Domain.Employee;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
