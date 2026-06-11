using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Services;
using TrainingMatrixApp.ViewModels;

namespace TrainingMatrixApp.Pages.Compliance;

public class IndexModel : PageModel
{
    private readonly DepartmentSkillComplianceService _complianceService;
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(DepartmentSkillComplianceService complianceService, TrainingMatrixDbContext context)
    {
        _complianceService = complianceService;
        _context = context;
    }

    public List<DepartmentSkillComplianceViewModel> ComplianceData { get; set; } = new();
    public int? DepartmentFilter { get; set; }
    public SelectList DepartmentList { get; set; } = default!;

    public async Task OnGetAsync(int? departmentFilter)
    {
        DepartmentFilter = departmentFilter;

        var departments = await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync();
        DepartmentList = new SelectList(departments, "Id", "Name");

        ComplianceData = await _complianceService.GetComplianceReportAsync(departmentFilter);
    }
}
