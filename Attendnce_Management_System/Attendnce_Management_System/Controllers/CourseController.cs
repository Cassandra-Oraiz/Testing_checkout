using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();
            if (!courses.Any())
                return NotFound("No Courses Found");

            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound($"#404! Course with ID {id} not found");

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseDTO dto)
        {
            var course = await _courseService.AddAsync(dto);
            return Ok(course);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, AddCourseDTO dto)
        {
            var updatedCourse = await _courseService.UpdateAsync(id, dto);
            if (updatedCourse == null)
                return NotFound($"#404! Course with ID {id} not found");

            return Ok(updatedCourse);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Course with ID {id} not found");

            return Ok($"Course with ID {id} deleted successfully");
        }
    }
}