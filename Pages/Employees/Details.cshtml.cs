using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Employees;

public class DetailsModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public DetailsModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public Employee Employee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Skills)
                .ThenInclude(es => es.Skill)
            .Include(e => e.Trainings)
                .ThenInclude(et => et.TrainingCourse)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            return NotFound();

        Employee = employee;
        return Page();
    }
}
