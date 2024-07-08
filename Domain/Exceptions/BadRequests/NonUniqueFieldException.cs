namespace Domain.Exceptions.BadRequests
{
    public class NonUniqueFieldException(string fieldName)
        : BadRequestException($"Field `{fieldName}` must be unique")
    {
    }
}
