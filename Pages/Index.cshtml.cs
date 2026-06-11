using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public int TotalDepartments { get; set; }
    public int TotalEmployees { get; set; }
    public int TotalSkills { get; set; }
    public int TotalCourses { get; set; }
    public List<AuditLog> RecentAuditLogs { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalDepartments = await _context.Departments.CountAsync(d => d.IsActive);
        TotalEmployees = await _context.Employees.CountAsync(e => e.IsActive);
        TotalSkills = await _context.Skills.CountAsync(s => s.IsActive);
        TotalCourses = await _context.TrainingCourses.CountAsync(tc => tc.IsActive);

        RecentAuditLogs = await _context.AuditLogs
            .OrderByDescending(a => a.Timestamp)
            .Take(10)
            .ToListAsync();
    }
}
