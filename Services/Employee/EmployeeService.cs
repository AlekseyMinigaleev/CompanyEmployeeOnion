using Domain.Company;
using Domain.Employee;
using Domain.Exceptions.NotFounds;
using Services.Employee.DTOs;

namespace Services.Employee
{
    internal class EmployeeService(
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository)
        : IEmployeeService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<EmployeeModel> CreateAsync(
            CreateEmployeeDTO dto,
            CancellationToken cancellationToken)
        {
            var companyModel = dto.CompanyId is null
                ? null
                : await _companyRepository
                    .GetByIdAsync((Guid)dto.CompanyId, cancellationToken);

            var employee = new EmployeeModel(
                firstName: dto.FirstName,
                lastName: dto.LastName,
                position: dto.Position,
                email: dto.Email,
                company: companyModel);

            var createdEmployee = await _employeeRepository
                .CreateAsync(employee, cancellationToken);

            return createdEmployee;
        }

        public async Task<EmployeeModel> UpdateAsync(
            UpdateEmployeeDto dto,
            CancellationToken cancellationToken)
        {
            var employeeToUpdate = await _employeeRepository
                .GetByIdAsync(dto.EmployeeId, cancellationToken)
                ?? throw new EntityNotFoundByIdException<EmployeeModel>(dto.EmployeeId);

            var company = dto.CompanyId is null
                    ? null
                    : await _companyRepository
                        .GetByIdAsync((Guid)dto.CompanyId, cancellationToken);

            employeeToUpdate.Update(
                firstName: dto.FirstName,
                lastName: dto.LastName,
                position: dto.Position,
                email: dto.Email,
                company: company);

            var updatedEmployee = await _employeeRepository
                .UpdateAsync(employeeToUpdate, cancellationToken);

            return updatedEmployee;
        }

        public async Task<EmployeeModel> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deletedEmployee = await _employeeRepository
                .DeleteAsync(id, cancellationToken);

            return
                deletedEmployee
                ?? throw new EntityNotFoundByIdException<EmployeeModel>(id);
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllAsync(
            CancellationToken cancellationToken) =>
            await _employeeRepository.GetAllAsync(cancellationToken);

        public async Task<EmployeeModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var employee = await
                _employeeRepository
                .GetByIdAsync(id, cancellationToken);

            return
                employee
                ?? throw new EntityNotFoundByIdException<EmployeeModel>(id);
        }
    }
}