namespace Services.Employee.DTOs
{
    public class CreateEmployeeDTO(
        string firstName,
        string lastName,
        string position,
        string email,
        Guid? companyId) : BaseEmployeeDTO(
            firstName,
            lastName,
            position,
            email,
            companyId)
    { }
}
