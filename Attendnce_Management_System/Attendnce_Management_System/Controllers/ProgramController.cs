using Microsoft.AspNetCore.Mvc;
using Attendance_Management_System.AttendanceManagementSystem.DTOs;
using Attendance_Management_System.AttendanceManagementSystem.Interface.ServiceInterface;

namespace Attendance_Management_System.AttendanceManagementSystem.Controllers
{
    [Route("AttendanceManagement/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrograms()
        {
            var programs = await _programService.GetAllAsync();
            if (!programs.Any())
                return NotFound("No Programs Found");

            return Ok(programs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProgramById(int id)
        {
            var program = await _programService.GetByIdAsync(id);
            if (program == null)
                return NotFound($"#404! Program with ID {id} not found");

            return Ok(program);
        }

        [HttpPost]
        public async Task<IActionResult> AddProgram(AddProgramDTO dto)
        {
            var program = await _programService.AddAsync(dto);
            return Ok(program);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProgram(int id, AddProgramDTO dto)
        {
            var updatedProgram = await _programService.UpdateAsync(id, dto);
            if (updatedProgram == null)
                return NotFound($"#404! Program with ID {id} not found");

            return Ok(updatedProgram);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var success = await _programService.DeleteAsync(id);
            if (!success)
                return NotFound($"#404! Program with ID {id} not found");

            return Ok($"Program with ID {id} deleted successfully");
        }
    }
}