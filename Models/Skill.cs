using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class Skill
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Skill Name")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [ValidateNever]
    public ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
    
    [ValidateNever]
    public ICollection<DepartmentSkillRequirement> DepartmentRequirements { get; set; } = new List<DepartmentSkillRequirement>();
}