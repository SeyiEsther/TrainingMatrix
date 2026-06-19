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
            .AsNoTracking()
            .Include(dsr => dsr.Department)
            .Include(dsr => dsr.Skill)
            .Where(dsr => dsr.IsActive && dsr.Department.IsActive);

        if (departmentId.HasValue)
        {
            requirementsQuery = requirementsQuery.Where(dsr => dsr.DepartmentId == departmentId.Value);
        }

        var requirements = await requirementsQuery.ToListAsync();

        var employeeSkills = await _context.EmployeeSkills
            .AsNoTracking()
            .Where(es => es.Employee.IsActive)
            .Select(es => new
            {
                es.Employee.DepartmentId,
                es.SkillId,
                es.ProficiencyLevel
            })
            .ToListAsync();

        var skillsByDepartment = employeeSkills
            .GroupBy(es => (es.DepartmentId, es.SkillId))
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ProficiencyLevel).ToList());

        var results = new List<DepartmentSkillComplianceViewModel>();

        foreach (var req in requirements)
        {
            var key = (req.DepartmentId, req.SkillId);
            var levels = skillsByDepartment.GetValueOrDefault(key, []);
            var qualifiedCount = levels.Count(level => level >= req.MinimumProficiencyLevel);

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
