using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            if (!enrollments.Any())
                return NotFound("No Enrollments Found");

            return Ok(enrollments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound($"#404! Enrollment with ID {id} not found");

            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollment(AddEnrollmentDTO dto)
        {
            var enrollment = await _enrollmentService.AddAsync(dto);
            return Ok(enrollment);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEnrollment(int id, AddEnrollmentDTO dto)
        {
            var updatedEnrollment = await _enrollmentService.UpdateAsync(id, dto);
            if (updatedEnrollment == null)
                return NotFound($"#404! Enrollment with ID {id} not found");

            return Ok(updatedEnrollment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var success = await _enrollmentService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Enrollment with ID {id} not found");

            return Ok($"Enrollment with ID {id} deleted successfully");
        }
    }
}