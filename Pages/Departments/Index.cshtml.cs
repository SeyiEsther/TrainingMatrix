using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Departments;

public class IndexModel : PageModel
{
    private const int PageSize = 5;
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public List<Department> TopLevelDepartments { get; set; } = new();
    public Dictionary<int, List<Department>> SubDepartmentsByParent { get; set; } = new();
    public Dictionary<int, int> EmployeeCountByDepartment { get; set; } = new();
    public string? SearchString { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;

    public async Task OnGetAsync(string? searchString, int page = 1)
    {
        SearchString = searchString;
        CurrentPage = Math.Max(1, page);

        var query = _context.Departments
            .AsNoTracking()
            .Where(d => d.IsActive && d.ParentDepartmentId == null);

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(d => d.Name.Contains(searchString));
        }

        var totalCount = await query.CountAsync();
        TotalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)PageSize));

        if (CurrentPage > TotalPages)
        {
            CurrentPage = TotalPages;
        }

        TopLevelDepartments = await query
            .Include(d => d.HeadOfDepartment)
            .Include(d => d.SubDepartments.Where(s => s.IsActive))
                .ThenInclude(s => s.HeadOfDepartment)
            .OrderBy(d => d.Name)
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        SubDepartmentsByParent = TopLevelDepartments
            .ToDictionary(
                d => d.Id,
                d => d.SubDepartments.Where(s => s.IsActive).OrderBy(s => s.Name).ToList());

        var departmentIds = TopLevelDepartments
            .SelectMany(d => d.SubDepartments.Where(s => s.IsActive).Select(s => s.Id))
            .Concat(TopLevelDepartments.Select(d => d.Id))
            .Distinct()
            .ToList();

        if (departmentIds.Count > 0)
        {
            EmployeeCountByDepartment = await _context.Employees
                .AsNoTracking()
                .Where(e => e.IsActive && departmentIds.Contains(e.DepartmentId))
                .GroupBy(e => e.DepartmentId)
                .Select(g => new { DepartmentId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.DepartmentId, x => x.Count);
        }
    }
}
