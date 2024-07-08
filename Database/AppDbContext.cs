using Domain.Company;
using Domain.Employee;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext(DbContextOptions options)
        : DbContext(options)
    {
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}