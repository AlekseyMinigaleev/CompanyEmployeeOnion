namespace Services.Company.DTOs
{
    public class BaseCompanyDTO(
        string name,
        string address,
        string industry,
        Guid[] employeeIds)
    {
        public string Name { get; set; } = name;

        public string Address { get; set; } = address;

        public string Industry { get; set; } = industry;

        public Guid[] EmployeeIds { get; set; } = employeeIds;
}
}
