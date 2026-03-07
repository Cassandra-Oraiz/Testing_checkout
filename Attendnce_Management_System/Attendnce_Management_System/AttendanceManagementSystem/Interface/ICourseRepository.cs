using Attendnce_Management_System.AttendanceManagementSystem.Model;

namespace Smart_Library.SmartLibraryManagement.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
    }
}