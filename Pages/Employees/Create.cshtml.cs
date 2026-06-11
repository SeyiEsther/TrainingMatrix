using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Employees;

public class CreateModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public CreateModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;

    public SelectList DepartmentList { get; set; } = default!;

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

        var existing = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeNumber == Employee.EmployeeNumber);
        if (existing != null)
        {
            ModelState.AddModelError("Employee.EmployeeNumber", "An employee with this number already exists.");
            await LoadDropdownsAsync();
            return Page();
        }

        Employee.IsActive = true;
        Employee.CreatedDate = DateTime.UtcNow;

        _context.Employees.Add(Employee);
        await _context.SaveChangesAsync();

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Add",
            EntityType = "Employee",
            EntityId = Employee.EmployeeNumber,
            Details = $"Employee {Employee.FullName} created.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Employee '{Employee.FullName}' has been created successfully.";
        return RedirectToPage("./Index");
    }

    private async Task LoadDropdownsAsync()
    {
        var departments = await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync();
        DepartmentList = new SelectList(departments, "Id", "Name");
    }
}
