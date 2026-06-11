using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Services;

public interface ITrainingMatrixService
{
    Task<List<Employee>> GetActiveEmployeesAsync();
    Task<List<Department>> GetActiveDepartmentsAsync();
    Task<List<Skill>> GetActiveSkillsAsync();
    Task<List<TrainingCourse>> GetActiveCoursesAsync();
}
