using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Skills;

public class EditModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public EditModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Skill Skill { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return NotFound();

        Skill = skill;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.Skills.FirstOrDefaultAsync(s => s.Name == Skill.Name && s.Id != Skill.Id);
        if (existing != null)
        {
            ModelState.AddModelError("Skill.Name", "A skill with this name already exists.");
            return Page();
        }

        _context.Attach(Skill).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Skills.AnyAsync(s => s.Id == Skill.Id))
                return NotFound();
            throw;
        }

        TempData["SuccessMessage"] = $"Skill '{Skill.Name}' has been updated successfully.";
        return RedirectToPage("./Index");
    }
}
