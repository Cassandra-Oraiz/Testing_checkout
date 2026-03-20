using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            if (!departments.Any())
                return NotFound("No Departments Found");

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound($"#404! Department with ID {id} not found");

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentDTO dto)
        {
            var department = await _departmentService.AddAsync(dto);
            return Ok(department);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment(int id, AddDepartmentDTO dto)
        {
            var updatedDepartment = await _departmentService.UpdateAsync(id, dto);
            if (updatedDepartment == null)
                return NotFound($"#404! Department with ID {id} not found");

            return Ok(updatedDepartment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var success = await _departmentService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Department with ID {id} not found");

            return Ok($"Department with ID {id} deleted successfully");
        }
    }
}