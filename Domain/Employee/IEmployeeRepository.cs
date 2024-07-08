namespace Domain.Employee
{
    public interface IEmployeeRepository : ICrudRepository<EmployeeModel>
    {
        public Task<IEnumerable<EmployeeModel>> GetByIdsAsync(
            Guid[] id,
            CancellationToken cancellationToken);
    }
}