namespace Domain
{
    public interface ICrudRepository<T>
        where T : BaseEntity
    {
        public Task<T> CreateAsync(
          T model,
          CancellationToken cancellationToken);

        public Task<T> UpdateAsync(
            T model,
            CancellationToken cancellationToken);

        public Task<IEnumerable<T>> GetAllAsync(
            CancellationToken cancellationToken);

        public Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<T> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
