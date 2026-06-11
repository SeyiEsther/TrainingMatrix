using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Skills;

public class DeleteModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public DeleteModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Skill Skill { get; set; } = default!;

    public int EmployeeSkillCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
            return NotFound();

        Skill = skill;
        EmployeeSkillCount = await _context.EmployeeSkills.CountAsync(es => es.SkillId == id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var skill = await _context.Skills.FindAsync(Skill.Id);
        if (skill == null)
            return NotFound();

        skill.IsActive = false;
        await _context.SaveChangesAsync();

        _context.AuditLogs.Add(new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ActionType = "Delete",
            EntityType = "Skill",
            EntityId = skill.Id.ToString(),
            Details = $"Skill '{skill.Name}' deactivated.",
            PerformedBy = User.Identity?.Name ?? "System"
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Skill '{skill.Name}' has been deactivated.";
        return RedirectToPage("./Index");
    }
}
