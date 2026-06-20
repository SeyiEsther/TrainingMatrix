using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;
using TrainingMatrixApp.Services;
using TrainingMatrixApp.ViewModels;

namespace TrainingMatrixApp.Pages;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;
    private readonly DepartmentSkillComplianceService _complianceService;

    public IndexModel(TrainingMatrixDbContext context, DepartmentSkillComplianceService complianceService)
    {
        _context = context;
        _complianceService = complianceService;
    }

    public int TotalDepartments { get; set; }
    public int TotalEmployees { get; set; }
    public int TotalSkills { get; set; }
    public int TotalCourses { get; set; }
    public int ComplianceMet { get; set; }
    public int ComplianceNearlyMet { get; set; }
    public int ComplianceNotMet { get; set; }
    public List<DepartmentSkillComplianceViewModel> AtRiskCompliance { get; set; } = new();
    public List<AuditLog> RecentAuditLogs { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalDepartments = await _context.Departments.CountAsync(d => d.IsActive);
        TotalEmployees = await _context.Employees.CountAsync(e => e.IsActive);
        TotalSkills = await _context.Skills.CountAsync(s => s.IsActive);
        TotalCourses = await _context.TrainingCourses.CountAsync(tc => tc.IsActive);

        var compliance = await _complianceService.GetComplianceReportAsync();
        ComplianceMet = compliance.Count(r => r.ComplianceStatus == "Met");
        ComplianceNearlyMet = compliance.Count(r => r.ComplianceStatus == "Nearly Met");
        ComplianceNotMet = compliance.Count(r => r.ComplianceStatus == "Not Met");
        AtRiskCompliance = compliance
            .Where(r => r.ComplianceStatus != "Met")
            .OrderByDescending(r => r.Priority == "Critical")
            .ThenByDescending(r => r.ShortfallCount)
            .Take(5)
            .ToList();

        RecentAuditLogs = await _context.AuditLogs
            .OrderByDescending(a => a.Timestamp)
            .Take(8)
            .ToListAsync();
    }
}
