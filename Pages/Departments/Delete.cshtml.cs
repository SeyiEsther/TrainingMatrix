using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;
using TrainingMatrixApp.Services;

namespace TrainingMatrixApp.Pages.Departments;

public class DeleteModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;
    private readonly IAuditService _auditService;

    public DeleteModel(TrainingMatrixDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    public int EmployeeCount { get; set; }
    public int SubDepartmentCount { get; set; }
    public bool CanDelete { get; set; }
    public List<string> DeletionBlockers { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .Include(d => d.ParentDepartment)
            .Include(d => d.HeadOfDepartment)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (department == null)
        {
            return NotFound();
        }

        Department = department;

        EmployeeCount = await _context.Employees.CountAsync(e => e.DepartmentId == id && e.IsActive);
        SubDepartmentCount = await _context.Departments.CountAsync(d => d.ParentDepartmentId == id && d.IsActive);

        CanDelete = true;

        if (EmployeeCount > 0)
        {
            CanDelete = false;
            DeletionBlockers.Add($"Department has {EmployeeCount} active employee(s)");
        }

        if (SubDepartmentCount > 0)
        {
            CanDelete = false;
            DeletionBlockers.Add($"Department has {SubDepartmentCount} active sub-department(s)");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FindAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        var employeeCount = await _context.Employees.CountAsync(e => e.DepartmentId == id && e.IsActive);
        var subDeptCount = await _context.Departments.CountAsync(d => d.ParentDepartmentId == id && d.IsActive);

        if (employeeCount > 0 || subDeptCount > 0)
        {
            TempData["ErrorMessage"] = "Cannot delete department with active employees or sub-departments.";
            return RedirectToPage("./Index");
        }

        department.IsActive = false;
        await _context.SaveChangesAsync();

        await _auditService.LogAsync(
            "Delete",
            "Department",
            department.Id.ToString(),
            $"Department '{department.Name}' deactivated.");

        TempData["SuccessMessage"] = $"Department '{department.Name}' has been deactivated.";
        return RedirectToPage("./Index");
    }
}
