using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Employees;

public class EditModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public EditModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;

    public SelectList DepartmentList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound();

        Employee = employee;
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

        var duplicate = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeNumber == Employee.EmployeeNumber && e.Id != Employee.Id);
        if (duplicate != null)
        {
            ModelState.AddModelError("Employee.EmployeeNumber", "An employee with this number already exists.");
            await LoadDropdownsAsync();
            return Page();
        }

        _context.Attach(Employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Employees.AnyAsync(e => e.Id == Employee.Id))
                return NotFound();
            throw;
        }

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Update",
            EntityType = "Employee",
            EntityId = Employee.EmployeeNumber,
            Details = $"Employee {Employee.FullName} updated.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Employee '{Employee.FullName}' has been updated successfully.";
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
