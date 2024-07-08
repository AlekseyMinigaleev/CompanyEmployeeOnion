namespace Services.Company.DTOs

{
    public class CreateCompany(
        string name,
        string address,
        string industry,
        Guid[] employeeIds) : BaseCompanyDTO(
            name,
            address,
            industry,
            employeeIds)
    { }
}