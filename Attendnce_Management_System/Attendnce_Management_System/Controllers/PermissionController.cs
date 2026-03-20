using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllAsync();
            if (!permissions.Any())
                return NotFound("No Permissions Found");

            return Ok(permissions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
                return NotFound($"#404! Permission with ID {id} not found");

            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(AddPermissionDTO dto)
        {
            var permission = await _permissionService.AddAsync(dto);
            return Ok(permission);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePermission(int id, AddPermissionDTO dto)
        {
            var updatedPermission = await _permissionService.UpdateAsync(id, dto);
            if (updatedPermission == null)
                return NotFound($"#404! Permission with ID {id} not found");

            return Ok(updatedPermission);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var success = await _permissionService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Permission with ID {id} not found");

            return Ok($"Permission with ID {id} deleted successfully");
        }
    }
}