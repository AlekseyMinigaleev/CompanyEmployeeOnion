using Domain;
using Domain.Company;
using Domain.Exceptions.NotFounds;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public abstract class BaseRepository<T>(AppDbContext dbContext) : ICrudRepository<T>
        where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext = dbContext;

        public virtual async Task<T> CreateAsync(T model, CancellationToken cancellationToken)
        {
            var createdModel = await _dbContext
                .Set<T>()
                .AddAsync(model, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return createdModel.Entity;
        }

        public virtual async Task<T> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var modelToDelete =
                await GetByIdAsync(id, cancellationToken)
                ?? throw new EntityNotFoundByIdException<CompanyModel>(id);

            var deletedModel = _dbContext
                .Set<T>()
                .Remove(modelToDelete);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return deletedModel.Entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) =>
            await _dbContext
                .Set<T>()
                .ToArrayAsync(cancellationToken);

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _dbContext
                .Set<T>()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public virtual async Task<T> UpdateAsync(T model, CancellationToken cancellationToken)
        {
            var updatedModel = _dbContext
                .Set<T>()
                .Update(model);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return updatedModel.Entity;
        }
    }
}