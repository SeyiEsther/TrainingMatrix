using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingTasks;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public List<TrainingTask> TrainingTasks { get; set; } = new();
    public string? SearchString { get; set; }
    public int? DepartmentFilter { get; set; }

    public async Task OnGetAsync(string? searchString, int? departmentId)
    {
        SearchString = searchString;
        DepartmentFilter = departmentId;

        var query = _context.TrainingTasks
            .AsNoTracking()
            .Include(t => t.Department)
            .Include(t => t.TrainingTaskSkills)
                .ThenInclude(ts => ts.Skill)
            .Where(t => t.IsActive);

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(t => t.Name.Contains(searchString));
        }

        if (departmentId.HasValue)
        {
            query = query.Where(t => t.DepartmentId == departmentId.Value);
        }

        TrainingTasks = await query
            .OrderBy(t => t.Department!.Name)
            .ThenBy(t => t.SortOrder ?? int.MaxValue)
            .ThenBy(t => t.Name)
            .ToListAsync();
    }
}
