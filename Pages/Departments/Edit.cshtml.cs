using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingMatrixApp.Data;
using TrainingMatrixApp.Models;

namespace TrainingMatrixApp.Pages.Departments;

public class EditModel : PageModel
{
    private readonly TrainingMatrixDbContext _context;

    public EditModel(TrainingMatrixDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    public SelectList ParentDepartmentList { get; set; } = default!;
    public SelectList HeadOfDepartmentList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
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
        await LoadDropdownsAsync(id.Value);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync(Department.Id);
            return Page();
        }

        // Check if department name already exists (excluding current department)
        var existingDept = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == Department.Name && d.Id != Department.Id && d.IsActive);

        if (existingDept != null)
        {
            ModelState.AddModelError("Department.Name", "A department with this name already exists.");
            await LoadDropdownsAsync(Department.Id);
            return Page();
        }

        // Prevent department from being its own parent
        if (Department.ParentDepartmentId == Department.Id)
        {
            ModelState.AddModelError("Department.ParentDepartmentId", "A department cannot be its own parent.");
            await LoadDropdownsAsync(Department.Id);
            return Page();
        }

        // Prevent circular reference (department cannot have itself as ancestor)
        if (Department.ParentDepartmentId.HasValue)
        {
            var hasCircularRef = await HasCircularReferenceAsync(Department.Id, Department.ParentDepartmentId.Value);
            if (hasCircularRef)
            {
                ModelState.AddModelError("Department.ParentDepartmentId", 
                    "Invalid parent department selection. This would create a circular reference.");
                await LoadDropdownsAsync(Department.Id);
                return Page();
            }
        }

        _context.Attach(Department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await DepartmentExistsAsync(Department.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        TempData["SuccessMessage"] = $"Department '{Department.Name}' has been updated successfully.";
        return RedirectToPage("./Index");
    }

    private async Task<bool> DepartmentExistsAsync(int id)
    {
        return await _context.Departments.AnyAsync(e => e.Id == id);
    }

    private async Task<bool> HasCircularReferenceAsync(int departmentId, int proposedParentId)
    {
        var currentParentId = proposedParentId;
        while (currentParentId != null)
        {
            if (currentParentId == departmentId)
            {
                return true;
            }

            var parent = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == currentParentId);

            currentParentId = parent?.ParentDepartmentId ?? 0;
            if (currentParentId == 0) break;
        }

        return false;
    }

    private async Task LoadDropdownsAsync(int currentDepartmentId)
    {
        // Load only top-level departments for parent selection (excluding current department)
        var parentDepartments = await _context.Departments
            .Where(d => d.IsActive && d.ParentDepartmentId == null && d.Id != currentDepartmentId)
            .OrderBy(d => d.Name)
            .ToListAsync();

        ParentDepartmentList = new SelectList(parentDepartments, "Id", "Name");

        // Load employees in this department for head of department
        var employees = await _context.Employees
            .Where(e => e.IsActive && (e.DepartmentId == currentDepartmentId || !_context.Departments.Any(d => d.HeadOfDepartmentId == e.Id)))
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .Select(e => new { e.Id, FullName = e.FirstName + " " + e.LastName + " (" + e.EmployeeNumber + ")" })
            .ToListAsync();

        HeadOfDepartmentList = new SelectList(employees, "Id", "FullName");
    }
}