using Microsoft.AspNetCore.Mvc;
using Services.Company;
using Services.Company.DTOs;

namespace Presentation.Controllers
{
    /// <summary>
    /// Контроллер для операций с компаниями.
    /// </summary>
    [ApiController]
    [Route("api/company")]
    public class CompanyController(
        ICompanyService companyService)
        : ControllerBase()
    {
        private readonly ICompanyService _companyService = companyService;

        /// <summary>
        /// Получает все компании.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список всех компаний.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken) =>
            Ok(await _companyService.GetAllAsync(cancellationToken));

        /// <summary>
        /// Получает компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Запись компании или 404, если не найдена.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken) =>
            Ok(await _companyService
                .GetByIdAsync(id, cancellationToken));

        /// <summary>
        /// Создает новую компанию.
        /// </summary>
        /// <param name="companyVM">ViewModel для создания компании.</param>
        /// <param name="validator">Валидатор для ViewModel создания компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданная запись компании.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateCompany createCompanyDto,
            CancellationToken cancellationToken)
        {
            var createdCompany = await _companyService
                .CreateAsync(createCompanyDto, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCompany.Id },
                createdCompany);
        }

        /// <summary>
        /// Удаляет компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Удаленная запись компании или 404, если не найдена.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken) =>
            Ok(await _companyService
                .DeleteAsync(id, cancellationToken));

        /// <summary>
        /// Обновляет компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании.</param>
        /// <param name="companyVM">ViewModel для обновления компании.</param>
        /// <param name="validator">Валидатор для ViewModel обновления компании.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленная запись компании или 404, если не найдена.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(
            Guid id,
            [FromBody] UpdateCompanyDTO updateCompanyDTO,
            CancellationToken cancellationToken)
        {
            updateCompanyDTO.CompanyId = id;

            var updatedEmployee = await _companyService
                .UpdateAsync(updateCompanyDTO, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = updatedEmployee.Id },
                updatedEmployee);
        }
    }
}