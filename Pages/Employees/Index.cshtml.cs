using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Employees;

public class IndexModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public IndexModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    public List<Employee> Employees { get; set; } = new();
    public string? SearchString { get; set; }
    public string? DepartmentFilter { get; set; }
    public SelectList DepartmentList { get; set; } = default!;

    public async Task OnGetAsync(string? searchString, string? departmentFilter)
    {
        SearchString = searchString;
        DepartmentFilter = departmentFilter;

        var departments = await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .ToListAsync();
        DepartmentList = new SelectList(departments, "Id", "Name");

        var query = _context.Employees
            .Include(e => e.Department)
            .Where(e => e.IsActive)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(e =>
                e.FirstName.Contains(searchString) ||
                e.LastName.Contains(searchString) ||
                e.EmployeeNumber.Contains(searchString));
        }

        if (!string.IsNullOrWhiteSpace(departmentFilter) && int.TryParse(departmentFilter, out var deptId))
        {
            query = query.Where(e => e.DepartmentId == deptId);
        }

        Employees = await query
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }
}
