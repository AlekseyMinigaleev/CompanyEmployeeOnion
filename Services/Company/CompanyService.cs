using Domain.Company;
using Domain.Employee;
using Domain.Exceptions.BadRequests;
using Domain.Exceptions.NotFounds;
using Services.Company.DTOs;

namespace Services.Company
{
    internal class CompanyService(
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository)
        : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<CompanyModel> CreateAsync(
            CreateCompany dto,
            CancellationToken cancellationToken)
        {
            if (IsDuplicateIds(dto.EmployeeIds))
                throw new DuplicateIdException(dto.EmployeeIds);

            var employees = await GetEmployeeByIdAsync(
                dto.EmployeeIds,
                cancellationToken);

            var companyModel = new CompanyModel(
                name: dto.Name,
                address: dto.Address,
                industry: dto.Industry,
                employees: employees);

            await _companyRepository
                .CreateAsync(companyModel, cancellationToken);

            return companyModel;
        }

        public async Task<CompanyModel> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var deletedCompany = await _companyRepository
                .DeleteAsync(id, cancellationToken);

            return deletedCompany is null
                ? throw new EntityNotFoundByIdException<CompanyModel>(id)
                : deletedCompany;
        }

        public async Task<IEnumerable<CompanyModel>> GetAllAsync(
            CancellationToken cancellationToken) =>
            await _companyRepository
                .GetAllAsync(cancellationToken);

        public async Task<CompanyModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken) =>
            await _companyRepository
                .GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundByIdException<CompanyModel>(id);

        public async Task<CompanyModel> UpdateAsync(
            UpdateCompanyDTO dto,
            CancellationToken cancellationToken)
        {
            var companyToUpdate = await _companyRepository
                .GetByIdAsync(dto.CompanyId, cancellationToken)
                ?? throw new EntityNotFoundByIdException<CompanyModel>(dto.CompanyId);

            if (IsDuplicateIds(dto.EmployeeIds))
                throw new DuplicateIdException(dto.EmployeeIds);

            var employees = await GetEmployeeByIdAsync(dto.EmployeeIds, cancellationToken);

            companyToUpdate.Update(
                name: dto.Name,
                address: dto.Address,
                industry: dto.Industry,
                employees: employees.ToArray()
                );

            var updatedCompany = await _companyRepository
                .UpdateAsync(companyToUpdate, cancellationToken);

            return updatedCompany;
        }

        private static bool IsDuplicateIds(Guid[] ids) =>
            ids.Distinct().Count() != ids.Length;

        private async Task<IEnumerable<EmployeeModel>> GetEmployeeByIdAsync(
            Guid[] ids,
            CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository
                .GetByIdsAsync(ids, cancellationToken);

            var missingIds = ids
                .Except(employees.Select(e => e.Id))
                .ToList();

            if (ids.Length != employees.Count())
                throw new EntityNotFoundByIdException<CompanyModel>(missingIds);

            return employees;
        }
    }
}