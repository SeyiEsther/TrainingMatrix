using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

[Table("Departments")]
public class Department
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public int? ParentDepartmentId { get; set; }
    
    [ValidateNever]
    public Department? ParentDepartment { get; set; }
    
    [ValidateNever]
    public ICollection<Department> SubDepartments { get; set; } = new List<Department>();
    
    public int? HeadOfDepartmentId { get; set; }
    
    [ValidateNever]
    public Employee? HeadOfDepartment { get; set; }
    
    [ValidateNever]
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    
    [ValidateNever]
    public ICollection<DepartmentSkillRequirement> SkillRequirements { get; set; } = new List<DepartmentSkillRequirement>();
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Full Path")]
    public string FullPath => ParentDepartment != null 
        ? $"{ParentDepartment.FullPath} > {Name}" 
        : Name;
    
    [Display(Name = "Department Type")]
    public string DepartmentType => ParentDepartmentId == null 
        ? "Main Department" 
        : "Sub-Department";

    // Add this property:
    public string? SapWorkCentre { get; set; }
}