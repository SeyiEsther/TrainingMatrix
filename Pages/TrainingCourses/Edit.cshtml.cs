using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingCourses;

public class EditModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public EditModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TrainingCourse Course { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _context.TrainingCourses.FindAsync(id);
        if (course == null)
            return NotFound();

        Course = course;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Attach(Course).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.TrainingCourses.AnyAsync(tc => tc.Id == Course.Id))
                return NotFound();
            throw;
        }

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Update",
            EntityType = "TrainingCourse",
            EntityId = Course.Id.ToString(),
            Details = $"Training course '{Course.Name}' updated.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Training course '{Course.Name}' has been updated.";
        return RedirectToPage("./Index");
    }
}
