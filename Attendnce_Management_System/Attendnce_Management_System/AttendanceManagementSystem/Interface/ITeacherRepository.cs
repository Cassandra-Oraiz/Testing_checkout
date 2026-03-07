using Attendnce_Management_System.AttendanceManagementSystem.Model;

namespace Smart_Library.SmartLibraryManagement.Interface
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher?> GetByIdAsync(int id);
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
    }
}