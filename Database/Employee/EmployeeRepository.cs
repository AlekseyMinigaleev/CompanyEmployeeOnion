using Domain.Employee;
using Microsoft.EntityFrameworkCore;

namespace Database.Employee
{
    public class EmployeeRepository(AppDbContext dbContext) 
        : BaseRepository<EmployeeModel>(dbContext), IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeModel>> GetByIdsAsync(
            Guid[] ids,
            CancellationToken cancellationToken) =>
            await _dbContext.Employees
                .Where(x => ids.Contains(x.Id))
                .ToArrayAsync(cancellationToken);
    }
}