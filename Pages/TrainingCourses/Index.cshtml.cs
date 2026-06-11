using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.TrainingCourses;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public List<TrainingCourse> Courses { get; set; } = new();
    public string? SearchString { get; set; }
    public string? CategoryFilter { get; set; }
    public SelectList CategoryList { get; set; } = default!;

    public async Task OnGetAsync(string? searchString, string? categoryFilter)
    {
        SearchString = searchString;
        CategoryFilter = categoryFilter;

        var allCategories = await _context.TrainingCourses
            .Where(tc => tc.IsActive && tc.Category != null)
            .Select(tc => tc.Category!)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();

        CategoryList = new SelectList(allCategories.Select(c => new { Value = c, Text = c }), "Value", "Text");

        var query = _context.TrainingCourses.Where(tc => tc.IsActive).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
            query = query.Where(tc => tc.Name.Contains(searchString) ||
                (tc.CourseCode != null && tc.CourseCode.Contains(searchString)) ||
                (tc.Provider != null && tc.Provider.Contains(searchString)));

        if (!string.IsNullOrWhiteSpace(categoryFilter))
            query = query.Where(tc => tc.Category == categoryFilter);

        Courses = await query.OrderBy(tc => tc.Name).ToListAsync();
    }
}
