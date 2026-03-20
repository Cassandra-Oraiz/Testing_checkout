using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendances()
        {
            var attendances = await _attendanceService.GetAllAsync();
            if (!attendances.Any())
                return NotFound("No Attendance Records Found");

            return Ok(attendances);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);
            if (attendance == null)
                return NotFound($"#404! Attendance with ID {id} not found");

            return Ok(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance(AddAttendanceDTO dto)
        {
            var attendance = await _attendanceService.AddAsync(dto);
            return Ok(attendance);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAttendance(int id, AddAttendanceDTO dto)
        {
            var updatedAttendance = await _attendanceService.UpdateAsync(id, dto);
            if (updatedAttendance == null)
                return NotFound($"#404! Attendance with ID {id} not found");

            return Ok(updatedAttendance);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var success = await _attendanceService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Attendance with ID {id} not found");

            return Ok($"Attendance with ID {id} deleted successfully");
        }
    }
}