using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.ViewModels;

namespace TrainingMatrixApp.Services;

public class DepartmentSkillComplianceService
{
    private readonly TrainingMatrixDbContext _context;

    public DepartmentSkillComplianceService(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentSkillComplianceViewModel>> GetComplianceReportAsync(int? departmentId = null)
    {
        var requirementsQuery = _context.DepartmentSkillRequirements
            .Include(dsr => dsr.Department)
            .Include(dsr => dsr.Skill)
            .Where(dsr => dsr.IsActive && dsr.Department.IsActive);

        if (departmentId.HasValue)
        {
            requirementsQuery = requirementsQuery.Where(dsr => dsr.DepartmentId == departmentId.Value);
        }

        var requirements = await requirementsQuery.ToListAsync();

        // Load employee skills for qualifying employees
        var employeeSkills = await _context.EmployeeSkills
            .Include(es => es.Employee)
            .Where(es => es.Employee.IsActive)
            .ToListAsync();

        var results = new List<DepartmentSkillComplianceViewModel>();

        foreach (var req in requirements)
        {
            // Count employees in this department who meet or exceed the minimum proficiency level for this skill
            var qualifiedCount = employeeSkills.Count(es =>
                es.Employee.DepartmentId == req.DepartmentId &&
                es.SkillId == req.SkillId &&
                es.ProficiencyLevel >= req.MinimumProficiencyLevel);

            var shortfall = Math.Max(0, req.RequiredCount - qualifiedCount);
            var percentage = req.RequiredCount > 0
                ? Math.Min(100, (decimal)qualifiedCount / req.RequiredCount * 100)
                : 100;

            string status;
            if (qualifiedCount >= req.RequiredCount)
                status = "Met";
            else if (qualifiedCount >= req.RequiredCount * 0.75m)
                status = "Nearly Met";
            else
                status = "Not Met";

            results.Add(new DepartmentSkillComplianceViewModel
            {
                DepartmentId = req.DepartmentId,
                DepartmentName = req.Department.Name,
                SkillId = req.SkillId,
                SkillName = req.Skill.Name,
                RequiredCount = req.RequiredCount,
                MinimumProficiencyLevel = req.MinimumProficiencyLevel,
                Priority = req.Priority,
                CurrentQualifiedCount = qualifiedCount,
                ShortfallCount = shortfall,
                ComplianceStatus = status,
                CompliancePercentage = Math.Round(percentage, 1)
            });
        }

        return results.OrderBy(r => r.DepartmentName).ThenBy(r => r.SkillName).ToList();
    }
}
