using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingMatrixApp.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly TrainingMatrixDbContext _context;
        private const int PageSize = 10;

        public DetailsModel(TrainingMatrixDbContext context)
        {
            _context = context;
        }

        public Department Department { get; set; } = default!;
        public List<Department> SubDepartments { get; set; } = new();
        public int TotalEmployees { get; set; }
        public List<AuditLog> AuditLogs { get; set; } = new();

        public PagedResult<Employee> PagedEmployees { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SortColumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortDirection { get; set; } // "asc" or "desc"

        public async Task<IActionResult> OnGetAsync(int? id, int? pageIndex, string? sortColumn, string? sortDirection)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Departments
                .Include(d => d.ParentDepartment)
                .Include(d => d.HeadOfDepartment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Department == null)
            {
                return NotFound();
            }

            SubDepartments = await _context.Departments
                .Where(d => d.ParentDepartmentId == Department.Id)
                .Include(d => d.HeadOfDepartment)
                .ToListAsync();

            // Employees for pagination and sorting
            var employeesQuery = _context.Employees
                .Where(e => e.DepartmentId == Department.Id);

            // Sorting logic
            SortColumn = string.IsNullOrEmpty(sortColumn) ? "LastName" : sortColumn;
            SortDirection = string.IsNullOrEmpty(sortDirection) ? "asc" : sortDirection.ToLower();

            employeesQuery = (SortColumn, SortDirection) switch
            {
                ("EmployeeNumber", "asc") => employeesQuery.OrderBy(e => e.EmployeeNumber),
                ("EmployeeNumber", "desc") => employeesQuery.OrderByDescending(e => e.EmployeeNumber),
                ("FullName", "asc") => employeesQuery.OrderBy(e => e.LastName).ThenBy(e => e.FirstName),
                ("FullName", "desc") => employeesQuery.OrderByDescending(e => e.LastName).ThenByDescending(e => e.FirstName),
                ("Email", "asc") => employeesQuery.OrderBy(e => e.Email),
                ("Email", "desc") => employeesQuery.OrderByDescending(e => e.Email),
                ("HireDate", "asc") => employeesQuery.OrderBy(e => e.HireDate),
                ("HireDate", "desc") => employeesQuery.OrderByDescending(e => e.HireDate),
                _ => employeesQuery.OrderBy(e => e.LastName).ThenBy(e => e.FirstName)
            };

            TotalEmployees = await employeesQuery.CountAsync();

            PageIndex = pageIndex ?? 1;
            var employees = await employeesQuery
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            PagedEmployees = new PagedResult<Employee>
            {
                Items = employees,
                PageIndex = PageIndex,
                PageSize = PageSize,
                TotalCount = TotalEmployees
            };

            AuditLogs = await _context.AuditLogs
                .Where(a => a.EntityType == "Department" && a.EntityId == Department.Id.ToString())
                .OrderByDescending(a => a.Timestamp)
                .Take(50)
                .ToListAsync();

            return Page();
        }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)System.Math.Ceiling((double)TotalCount / PageSize);
    }
}