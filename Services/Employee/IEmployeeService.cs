using Domain.Employee;
using Services.Employee.DTOs;

namespace Services.Employee
{
    public interface IEmployeeService
    {
        public Task<EmployeeModel> CreateAsync(
            CreateEmployeeDTO dto,
            CancellationToken cancellationToken);

        public Task<EmployeeModel> UpdateAsync(
            UpdateEmployeeDto dto,
            CancellationToken cancellationToken);

        public Task<IEnumerable<EmployeeModel>> GetAllAsync(
            CancellationToken cancellationToken);

        public Task<EmployeeModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<EmployeeModel> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}