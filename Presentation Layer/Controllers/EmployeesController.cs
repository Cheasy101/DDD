using Application.ApplicationLayer.Dto;
using Application.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace DDD.APP.Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Add(EmployeeDto employeeDto)
        {
            await employeeService.AddAsync(employeeDto);
            return CreatedAtAction(nameof(GetById), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id) return BadRequest();
            await employeeService.UpdateAsync(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}