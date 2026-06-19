using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;
using TrainingMatrixApp.Services;

namespace TrainingMatrixApp.Pages.Departments;

public class CreateModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;
    private readonly IAuditService _auditService;

    public CreateModel(TrainingMatrixDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    public SelectList ParentDepartmentList { get; set; } = default!;
    public SelectList HeadOfDepartmentList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadDropdownsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return Page();
        }

        var existingDept = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == Department.Name && d.IsActive);

        if (existingDept != null)
        {
            ModelState.AddModelError("Department.Name", "A department with this name already exists.");
            await LoadDropdownsAsync();
            return Page();
        }

        Department.IsActive = true;
        Department.CreatedDate = DateTime.UtcNow;

        _context.Departments.Add(Department);
        await _context.SaveChangesAsync();

        await _auditService.LogAsync(
            "Add",
            "Department",
            Department.Id.ToString(),
            $"Department '{Department.Name}' created.");

        TempData["SuccessMessage"] = $"Department '{Department.Name}' has been created successfully.";
        return RedirectToPage("./Index");
    }

    private async Task LoadDropdownsAsync()
    {
        var parentDepartments = await _context.Departments
            .Where(d => d.IsActive && d.ParentDepartmentId == null)
            .OrderBy(d => d.Name)
            .ToListAsync();

        ParentDepartmentList = new SelectList(parentDepartments, "Id", "Name");

        var employees = await _context.Employees
            .Where(e => e.IsActive)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .Select(e => new { e.Id, FullName = e.FirstName + " " + e.LastName + " (" + e.EmployeeNumber + ")" })
            .ToListAsync();

        HeadOfDepartmentList = new SelectList(employees, "Id", "FullName");
    }
}
