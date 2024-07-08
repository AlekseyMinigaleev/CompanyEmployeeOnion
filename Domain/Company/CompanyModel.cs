using Domain.Employee;
using Domain.Exceptions.BadRequests;

namespace Domain.Company
{
    /// <summary>
    /// Модель компании.
    /// </summary>
    public class CompanyModel : BaseEntity
    {
        /// <summary>
        /// Название компании.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Адрес компании.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Отрасль компании.
        /// </summary>
        public string Industry { get; private set; }

        /// <summary>
        /// Список сотрудников компании.
        /// </summary>
        public ICollection<EmployeeModel> Employees { get; private set; } = [];

        public CompanyModel(
            string name,
            string address,
            string industry,
            IEnumerable<EmployeeModel> employees)
        {
            PopulateCompanyInfo(
                name: name,
                address: address,
                industry: industry,
                employees: employees);
        }

        private CompanyModel()
        { }

        /// <summary>
        /// Обновляет данные компании на основе переданных параметров.
        /// </summary>
        /// <param name="name">Новое название компании.</param>
        /// <param name="address">Новый адрес компании.</param>
        /// <param name="industry">Новая отрасль компании.</param>
        public void Update(
            string name,
            string address,
            string industry,
            IEnumerable<EmployeeModel> employees)
        {
            PopulateCompanyInfo(
                name: name,
                address: address,
                industry: industry,
                employees: employees);

            if (Employees.Count != 0)
                Employees
                .Where(emp => !employees.Any(e => e.Id == emp.Id))
                .ToList()
                .ForEach(x => x.SetCompany(null));
        }

        /// <summary>
        /// Устанавливает список сотрудников компании и обновляет их принадлежность к текущей компании.
        /// </summary>
        /// <param name="employees">Список сотрудников компании.</param>
        public void AddEmployees(IEnumerable<EmployeeModel> employees) =>
                employees.ToList()
                    .ForEach(x => x.SetCompany(this));

        private void PopulateCompanyInfo(
            string name,
            string address,
            string industry,
            IEnumerable<EmployeeModel> employees)
        {
            Name = string.IsNullOrEmpty(name)
                    ? throw new ArgumentNullOrEmptyException(nameof(name))
                    : name;

            Address = string.IsNullOrEmpty(address)
                ? throw new ArgumentNullOrEmptyException(nameof(name))
                : name;

            Industry = string.IsNullOrEmpty(industry)
                ? throw new ArgumentNullOrEmptyException(nameof(industry))
                : industry;

            AddEmployees(employees);
        }
    }
}