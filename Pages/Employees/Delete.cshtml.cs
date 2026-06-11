using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Employees;

public class DeleteModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public DeleteModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            return NotFound();

        Employee = employee;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var employee = await _context.Employees.FindAsync(Employee.Id);
        if (employee == null)
            return NotFound();

        employee.IsActive = false;
        await _context.SaveChangesAsync();

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Delete",
            EntityType = "Employee",
            EntityId = employee.EmployeeNumber,
            Details = $"Employee {employee.FullName} deactivated.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Employee '{employee.FullName}' has been deactivated.";
        return RedirectToPage("./Index");
    }
}
