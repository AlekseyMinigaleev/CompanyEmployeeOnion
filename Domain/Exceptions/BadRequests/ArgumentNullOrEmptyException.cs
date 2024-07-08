namespace Domain.Exceptions.BadRequests
{
    public class ArgumentNullOrEmptyException(string fieldName) 
        : BadRequestException($"{fieldName}: must not have null or empty value")
    { }
}
