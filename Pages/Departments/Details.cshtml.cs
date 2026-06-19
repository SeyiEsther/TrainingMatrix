using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;
using TrainingMatrixApp.Services;

namespace TrainingMatrixApp.Pages.Departments;

public class DetailsModel : PageModel
{
    private const int PageSize = 10;
    private readonly TrainingMatrixDbContext _context;
    private readonly IAuditService _auditService;

    public DetailsModel(TrainingMatrixDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public Department Department { get; set; } = default!;
    public List<Department> SubDepartments { get; set; } = new();
    public List<DepartmentSkillRequirement> SkillRequirements { get; set; } = new();
    public int TotalEmployees { get; set; }
    public List<AuditLog> AuditLogs { get; set; } = new();
    public SelectList SkillList { get; set; } = default!;
    public PagedResult<Employee> PagedEmployees { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int PageIndex { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string? SortColumn { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SortDirection { get; set; }

    [BindProperty]
    public int NewSkillId { get; set; }

    [BindProperty]
    public int NewRequiredCount { get; set; } = 1;

    [BindProperty]
    public int NewMinimumProficiencyLevel { get; set; } = 1;

    [BindProperty]
    public string NewPriority { get; set; } = "Medium";

    public async Task<IActionResult> OnGetAsync(int? id, int? pageIndex, string? sortColumn, string? sortDirection)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments
            .Include(d => d.ParentDepartment)
            .Include(d => d.HeadOfDepartment)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (department == null)
        {
            return NotFound();
        }

        Department = department;
        await LoadPageDataAsync(pageIndex, sortColumn, sortDirection);
        return Page();
    }

    public async Task<IActionResult> OnPostAddSkillRequirementAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        Department = department;

        var existing = await _context.DepartmentSkillRequirements
            .FirstOrDefaultAsync(r => r.DepartmentId == id && r.SkillId == NewSkillId && r.IsActive);

        if (existing != null)
        {
            TempData["ErrorMessage"] = "This skill requirement already exists for the department.";
            return RedirectToPage(new { id });
        }

        var skill = await _context.Skills.FindAsync(NewSkillId);
        if (skill == null)
        {
            TempData["ErrorMessage"] = "Selected skill was not found.";
            return RedirectToPage(new { id });
        }

        var requirement = new DepartmentSkillRequirement
        {
            DepartmentId = id,
            SkillId = NewSkillId,
            RequiredCount = NewRequiredCount,
            MinimumProficiencyLevel = NewMinimumProficiencyLevel,
            Priority = NewPriority,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        _context.DepartmentSkillRequirements.Add(requirement);
        await _context.SaveChangesAsync();

        await _auditService.LogAsync(
            "Add",
            "Department",
            id.ToString(),
            $"Skill requirement added: {skill.Name} ({NewRequiredCount} at level {NewMinimumProficiencyLevel}+).");

        TempData["SuccessMessage"] = $"Skill requirement for '{skill.Name}' added.";
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostRemoveSkillRequirementAsync(int id, int requirementId)
    {
        var requirement = await _context.DepartmentSkillRequirements
            .Include(r => r.Skill)
            .FirstOrDefaultAsync(r => r.Id == requirementId && r.DepartmentId == id);

        if (requirement == null)
        {
            return NotFound();
        }

        requirement.IsActive = false;
        requirement.LastUpdated = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        await _auditService.LogAsync(
            "Delete",
            "Department",
            id.ToString(),
            $"Skill requirement removed: {requirement.Skill.Name}.");

        TempData["SuccessMessage"] = $"Skill requirement for '{requirement.Skill.Name}' removed.";
        return RedirectToPage(new { id });
    }

    private async Task LoadPageDataAsync(int? pageIndex, string? sortColumn, string? sortDirection)
    {
        SubDepartments = await _context.Departments
            .Where(d => d.ParentDepartmentId == Department.Id && d.IsActive)
            .Include(d => d.HeadOfDepartment)
            .ToListAsync();

        SkillRequirements = await _context.DepartmentSkillRequirements
            .Where(r => r.DepartmentId == Department.Id && r.IsActive)
            .Include(r => r.Skill)
            .OrderBy(r => r.Skill.Name)
            .ToListAsync();

        var assignedSkillIds = SkillRequirements.Select(r => r.SkillId).ToHashSet();
        var availableSkills = await _context.Skills
            .Where(s => s.IsActive && !assignedSkillIds.Contains(s.Id))
            .OrderBy(s => s.Name)
            .ToListAsync();
        SkillList = new SelectList(availableSkills, "Id", "Name");

        var employeesQuery = _context.Employees
            .Where(e => e.DepartmentId == Department.Id && e.IsActive);

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
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
