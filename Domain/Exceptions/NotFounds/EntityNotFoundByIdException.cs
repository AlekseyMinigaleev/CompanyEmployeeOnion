namespace Domain.Exceptions.NotFounds
{
    public class EntityNotFoundByIdException<TEntity>
        : NotFoundException
    {
        public EntityNotFoundByIdException(Guid entityId)
            : base($"Entity of type {nameof(TEntity)} " +
                  $"was not found " +
                  $"by Id: {entityId}")
        { }

        public EntityNotFoundByIdException(IEnumerable<Guid> ids)
            : base($"Entity of type {nameof(TEntity)} " +
                  $"was not found " +
                  $"by Ids: {string.Join(", ", ids)}")
        { }
    }
}