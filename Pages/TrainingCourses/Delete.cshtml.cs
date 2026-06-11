using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingCourses;

public class DeleteModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public DeleteModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TrainingCourse Course { get; set; } = default!;

    public int EnrolledCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _context.TrainingCourses.FindAsync(id);
        if (course == null)
            return NotFound();

        Course = course;
        EnrolledCount = await _context.EmployeeTrainings.CountAsync(et => et.TrainingCourseId == id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var course = await _context.TrainingCourses.FindAsync(Course.Id);
        if (course == null)
            return NotFound();

        course.IsActive = false;
        await _context.SaveChangesAsync();

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Delete",
            EntityType = "TrainingCourse",
            EntityId = course.Id.ToString(),
            Details = $"Training course '{course.Name}' deactivated.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Training course '{course.Name}' has been deactivated.";
        return RedirectToPage("./Index");
    }
}
