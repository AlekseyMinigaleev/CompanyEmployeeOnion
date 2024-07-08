using System.Text.Json.Serialization;

namespace Services.Company.DTOs
{
    public class UpdateCompanyDTO(
    string name,
        string address,
        string industry,
        Guid[] employeeIds,
        Guid companyId) : BaseCompanyDTO(
            name,
            address,
            industry,
            employeeIds)
    {
        [JsonIgnore]
        public Guid CompanyId { get; set; } = companyId;
    }
}