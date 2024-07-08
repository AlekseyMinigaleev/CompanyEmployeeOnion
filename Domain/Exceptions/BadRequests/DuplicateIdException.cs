namespace Domain.Exceptions.BadRequests
{
    public class DuplicateIdException(
        IEnumerable<Guid> ids)
        : BadRequestException(
            $"List of Ids must be unique.{ids}")
    {
    }
}