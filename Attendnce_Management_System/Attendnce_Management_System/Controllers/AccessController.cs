using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    /// <summary>
    /// Handles all operations related to users.
    /// </summary>
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }


        /// <summary>
        /// Kanade
        /// </summary>
        /// <remarks>
        /// KANAADEEEEE
        /// This endpoint retrieves all access records from the database.
        /// </remarks>
        /// <returns>List of access records</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAccesses()
        {
            var accesses = await _accessService.GetAllAsync();
            if (!accesses.Any())
                return NotFound("No Access Records Found");

            return Ok(accesses);
        }

        /// <summary>
        /// Kanade
        /// </summary>
        /// <param name="id"></param>
        /// <returns>KANAAHHDEEEEEEEEE</returns>

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessById(int id)
        {
            var access = await _accessService.GetByIdAsync(id);
            if (access == null)
                return NotFound($"#404! Access with ID {id} not found");

            return Ok(access);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccess(AddAccessDTO dto)
        {
            var access = await _accessService.AddAsync(dto);
            return Ok(access);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAccess(int id, AddAccessDTO dto)
        {
            var updatedAccess = await _accessService.UpdateAsync(id, dto);
            if (updatedAccess == null)
                return NotFound($"#404! Access with ID {id} not found");

            return Ok(updatedAccess);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccess(int id)
        {
            var success = await _accessService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Access with ID {id} not found");

            return Ok($"Access with ID {id} deleted successfully");
        }
    }
}