using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Skills;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public List<Skill> Skills { get; set; } = new();
    public string? SearchString { get; set; }
    public string? CategoryFilter { get; set; }
    public SelectList CategoryList { get; set; } = default!;

    public async Task OnGetAsync(string? searchString, string? categoryFilter)
    {
        SearchString = searchString;
        CategoryFilter = categoryFilter;

        var allCategories = await _context.Skills
            .Where(s => s.IsActive)
            .Select(s => s.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        CategoryList = new SelectList(allCategories.Select(c => new { Value = c, Text = c }), "Value", "Text");

        var query = _context.Skills.Where(s => s.IsActive).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
            query = query.Where(s => s.Name.Contains(searchString) || (s.Description != null && s.Description.Contains(searchString)));

        if (!string.IsNullOrWhiteSpace(categoryFilter))
            query = query.Where(s => s.Category == categoryFilter);

        Skills = await query.OrderBy(s => s.Category).ThenBy(s => s.Name).ToListAsync();
    }
}
