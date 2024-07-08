using System.Text.Json.Serialization;

namespace Services.Employee.DTOs
{
    public class UpdateEmployeeDto(
        string firstName,
        string lastName,
        string position,
        string email,
        Guid? companyId,
        Guid employeeId) : BaseEmployeeDTO(
            firstName,
            lastName,
            position,
            email,
            companyId)
    {
        [JsonIgnore]
        public Guid EmployeeId { get; set; } = employeeId;
    }
}