namespace Services.Employee.DTOs
{
    public class BaseEmployeeDTO(
        string firstName,
        string lastName,
        string position,
        string email,
        Guid? companyId)
    {
        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastName;

        public string Position { get; set; } = position;

        public string Email { get; set; } = email;

        public Guid? CompanyId { get; set; } = companyId;
    }
}
