using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingCourses;

public class CreateModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public CreateModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public TrainingCourse Course { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        if (!string.IsNullOrWhiteSpace(Course.CourseCode))
        {
            var existing = await _context.TrainingCourses.FirstOrDefaultAsync(tc => tc.CourseCode == Course.CourseCode);
            if (existing != null)
            {
                ModelState.AddModelError("Course.CourseCode", "A course with this code already exists.");
                return Page();
            }
        }

        Course.IsActive = true;
        Course.CreatedDate = DateTime.UtcNow;

        _context.TrainingCourses.Add(Course);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Training course '{Course.Name}' has been created successfully.";
        return RedirectToPage("./Index");
    }
}
