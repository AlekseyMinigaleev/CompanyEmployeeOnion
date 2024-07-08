namespace Domain.Exceptions.BadRequests
{
    public abstract class BadRequestException(string message)
        : Exception(message)
    {
    }
}