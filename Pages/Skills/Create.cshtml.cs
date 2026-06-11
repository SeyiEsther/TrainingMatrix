using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Skills;

public class CreateModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public CreateModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Skill Skill { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.Skills.FirstOrDefaultAsync(s => s.Name == Skill.Name);
        if (existing != null)
        {
            ModelState.AddModelError("Skill.Name", "A skill with this name already exists.");
            return Page();
        }

        Skill.IsActive = true;
        Skill.CreatedDate = DateTime.UtcNow;

        _context.Skills.Add(Skill);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Skill '{Skill.Name}' has been created successfully.";
        return RedirectToPage("./Index");
    }
}
