using Domain.Company;
using Services.Company.DTOs;

namespace Services.Company
{
    public interface ICompanyService
    {
        public Task<CompanyModel> CreateAsync(
            CreateCompany dto,
            CancellationToken cancellationToken);

        public Task<CompanyModel> UpdateAsync(
            UpdateCompanyDTO dto,
            CancellationToken cancellationToken);

        public Task<IEnumerable<CompanyModel>> GetAllAsync(
            CancellationToken cancellationToken);

        public Task<CompanyModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<CompanyModel> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}