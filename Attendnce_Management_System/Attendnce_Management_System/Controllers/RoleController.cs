using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllAsync();
            if (!roles.Any())
                return NotFound("No Roles Found");

            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
                return NotFound($"#404! Role with ID {id} not found");

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleDTO dto)
        {
            var role = await _roleService.AddAsync(dto);
            return Ok(role);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, AddRoleDTO dto)
        {
            var updatedRole = await _roleService.UpdateAsync(id, dto);
            if (updatedRole == null)
                return NotFound($"#404! Role with ID {id} not found");

            return Ok(updatedRole);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var success = await _roleService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Role with ID {id} not found");

            return Ok($"Role with ID {id} deleted successfully");
        }
    }
}