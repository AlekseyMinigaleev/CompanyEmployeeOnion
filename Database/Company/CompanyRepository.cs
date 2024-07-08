using Domain.Company;
namespace Database.Company
{
    public class CompanyRepository(AppDbContext dbContext)
        : BaseRepository<CompanyModel>(dbContext), ICompanyRepository
    { }
}