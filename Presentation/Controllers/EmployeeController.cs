using Microsoft.AspNetCore.Mvc;
using Services.Employee;
using Services.Employee.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController(
        IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        /// <summary>
        /// Получает список всех сотрудников.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список всех сотрудников.</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken) =>
            Ok(await _employeeService
                .GetAllAsync(cancellationToken));

        /// <summary>
        /// Получает информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о сотруднике или NotFound, если сотрудник не найден.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken) =>
            Ok(await _employeeService
                .GetByIdAsync(id, cancellationToken));

        /// <summary>
        /// Создает нового сотрудника.
        /// </summary>
        /// <param name="createEmployeeDTO">ViewModel для создания сотрудника.</param>
        /// <param name="validator">Валидатор для ViewModel создания сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Созданный сотрудник.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateEmployeeDTO createEmployeeDTO,
            CancellationToken cancellationToken)
        {
            var createdEmployee = await _employeeService
                .CreateAsync(createEmployeeDTO, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.Id },
                createdEmployee);
        }

        /// <summary>
        /// Удаляет сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Удаленный сотрудник или NotFound, если сотрудник не найден.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(
            Guid id,
            CancellationToken cancellationToken) =>
            Ok(await _employeeService
                .DeleteAsync(id, cancellationToken));

        /// <summary>
        /// Обновляет информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <param name="updateEmployeeVm">ViewModel для обновления информации о сотруднике.</param>
        /// <param name="validator">Валидатор для ViewModel обновления информации о сотруднике.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Обновленная информация о сотруднике или NotFound, если сотрудник не найден.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByIdAsync(
            Guid id,
            [FromBody] UpdateEmployeeDto updateEmployeeVm,
            CancellationToken cancellationToken)
        {
            updateEmployeeVm.EmployeeId = id;

            var updatedEmployee = await _employeeService
                .UpdateAsync(updateEmployeeVm, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = updatedEmployee.Id },
                updatedEmployee);
        }
    }
}