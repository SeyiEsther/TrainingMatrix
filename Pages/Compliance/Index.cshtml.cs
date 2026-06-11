using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Services;
using TrainingMatrixApp.ViewModels;

namespace TrainingMatrixApp.Pages.Compliance;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;
    private readonly DepartmentSkillComplianceService _complianceService;

    public IndexModel(TrainingMatrixDbContext context, DepartmentSkillComplianceService complianceService)
    {
        _context = context;
        _complianceService = complianceService;
    }

    public List<DepartmentSkillComplianceViewModel> ComplianceData { get; set; } = new();
    public SelectList DepartmentList { get; set; } = default!;
    public int? SelectedDepartmentId { get; set; }

    public async Task OnGetAsync(int? departmentId)
    {
        SelectedDepartmentId = departmentId;

        var departments = await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync();
        DepartmentList = new SelectList(departments, "Id", "Name");

        ComplianceData = await _complianceService.GetComplianceReportAsync(departmentId);
    }
}
