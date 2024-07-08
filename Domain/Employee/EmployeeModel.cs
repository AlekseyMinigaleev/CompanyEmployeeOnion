﻿using Domain.Company;
using Domain.Exceptions.BadRequests;

namespace Domain.Employee
{
    /// <summary>
    /// Модель сотрудника.
    /// </summary>
    public class EmployeeModel : BaseEntity
    {
        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Электронная почта сотрудника.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Дата начала работы сотрудника в компании.
        /// </summary>
        public DateTime? EmploymentDate { get; private set; }

        /// <summary>
        /// Дата увольнения сотрудника.
        /// </summary>
        public DateTime? TerminationDate { get; private set; }

        /// <summary>
        /// Уникальный идентификатор компании, к которой принадлежит сотрудник.
        /// </summary>
        public Guid? CompanyId { get; private set; }

        /// <summary>
        /// Компания, к которой принадлежит сотрудник.
        /// </summary>
        public CompanyModel? Company { get; private set; }

        public EmployeeModel(
            string firstName,
            string lastName,
            string position,
            string email,
            CompanyModel? company)
        {
            Update(
                firstName: firstName,
                lastName: lastName,
                position: position,
                email: email,
                company: company);
        }

        private EmployeeModel()
        { }

        /// <summary>
        /// Устанавливает компанию, к которой принадлежит сотрудник.
        /// Если <paramref name="company"/> равно <see langword="null"/>, сотрудник увольняется из текущей компании.
        /// </summary>
        /// <param name="company">Компания, к которой принадлежит сотрудник.</param>
        public void SetCompany(CompanyModel? company)
        {
            if (company is null && Company is not null)
            {
                TerminationDate = DateTime.UtcNow;
            }
            else if (company is null)
            {
                EmploymentDate = null;
            }
            else
            {
                EmploymentDate = DateTime.UtcNow;
                TerminationDate = null;
            }

            Company = company;
            CompanyId = company?.Id;
        }

        /// <summary>
        /// Обновляет информацию о сотруднике на основе переданных параметров.
        /// </summary>
        /// <param name="firstName">Новое имя сотрудника.</param>
        /// <param name="lastName">Новая фамилия сотрудника.</param>
        /// <param name="position">Новая должность сотрудника.</param>
        public void Update(
            string firstName,
            string lastName,
            string position,
            string email,
            CompanyModel? company)
        {
            FirstName = string.IsNullOrEmpty(firstName)
                    ? throw new ArgumentNullOrEmptyException(nameof(firstName))
                    : firstName;
            LastName = string.IsNullOrEmpty(lastName)
                    ? throw new ArgumentNullOrEmptyException(nameof(lastName))
                    : lastName;

            Position = string.IsNullOrEmpty(position)
                ? throw new ArgumentNullOrEmptyException(nameof(position))
                : position;

            Email = string.IsNullOrEmpty(email)
                ? throw new ArgumentNullOrEmptyException(nameof(email))
                : email;

            SetCompany(company);
        }
    }
}