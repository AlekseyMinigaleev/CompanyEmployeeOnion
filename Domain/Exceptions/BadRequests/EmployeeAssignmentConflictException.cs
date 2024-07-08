namespace Domain.Exceptions.BadRequests
{
    public class EmployeeAssignmentConflictException(
        Guid employeeId,
        Guid companyId)
        : BadRequestException(
            $"Attempting to assign employee with id '{employeeId}'" +
            $" to a new company," +
            $" but they are already assigned to company with id'{companyId}'.")
    {
    }
}
