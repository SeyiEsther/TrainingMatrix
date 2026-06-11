using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Services;

public class TrainingMatrixService : ITrainingMatrixService
{
    private readonly TrainingMatrixDbContext _context;

    public TrainingMatrixService(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetActiveEmployeesAsync()
    {
        return await _context.Employees
            .Where(e => e.IsActive)
            .Include(e => e.Department)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<List<Department>> GetActiveDepartmentsAsync()
    {
        return await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<List<Skill>> GetActiveSkillsAsync()
    {
        return await _context.Skills
            .Where(s => s.IsActive)
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<List<TrainingCourse>> GetActiveCoursesAsync()
    {
        return await _context.TrainingCourses
            .Where(tc => tc.IsActive)
            .OrderBy(tc => tc.Name)
            .ToListAsync();
    }
}
