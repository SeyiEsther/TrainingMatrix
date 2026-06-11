using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingCourses;

public class DetailsModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public DetailsModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public TrainingCourse Course { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var course = await _context.TrainingCourses
            .Include(tc => tc.EmployeeTrainings)
                .ThenInclude(et => et.Employee)
            .FirstOrDefaultAsync(tc => tc.Id == id);

        if (course == null)
            return NotFound();

        Course = course;
        return Page();
    }
}
