namespace Domain.Exceptions.NotFounds
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }
}
