using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Departments;

public class CreateModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public CreateModel(TrainingMatrixDbContext context)
    {
        _context = context;
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

        // Check if department name already exists
        var existingDept = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == Department.Name && d.IsActive);

        if (existingDept != null)
        {
            ModelState.AddModelError("Department.Name", "A department with this name already exists.");
            await LoadDropdownsAsync();
            return Page();
        }

        // Validate head of department is in the selected department (if sub-level)
        if (Department.HeadOfDepartmentId.HasValue && Department.ParentDepartmentId.HasValue)
        {
            var employee = await _context.Employees.FindAsync(Department.HeadOfDepartmentId.Value);
            if (employee != null && employee.DepartmentId != Department.Id)
            {
                // This will be set after creation, so we'll skip this validation for now
            }
        }

        Department.IsActive = true;
        Department.CreatedDate = DateTime.UtcNow;

        _context.Departments.Add(Department);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Department '{Department.Name}' has been created successfully.";
        return RedirectToPage("./Index");
    }

    private async Task LoadDropdownsAsync()
    {
        // Load only top-level departments for parent selection
        var parentDepartments = await _context.Departments
            .Where(d => d.IsActive && d.ParentDepartmentId == null)
            .OrderBy(d => d.Name)
            .ToListAsync();

        ParentDepartmentList = new SelectList(parentDepartments, "Id", "Name");

        // Load all active employees for head of department
        var employees = await _context.Employees
            .Where(e => e.IsActive)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .Select(e => new { e.Id, FullName = e.FirstName + " " + e.LastName + " (" + e.EmployeeNumber + ")" })
            .ToListAsync();

        HeadOfDepartmentList = new SelectList(employees, "Id", "FullName");
    }
}