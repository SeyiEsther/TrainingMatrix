using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class DepartmentSkillRequirement
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }
    
    [ValidateNever]
    public Department Department { get; set; } = null!;
    
    [Required]
    [Display(Name = "Skill")]
    public int SkillId { get; set; }
    
    [ValidateNever]
    public Skill Skill { get; set; } = null!;
    
    [Required]
    [Range(1, 100)]
    [Display(Name = "Required Employee Count")]
    public int RequiredCount { get; set; } // Number of employees needed with this skill
    
    [Required]
    [Range(1, 5)]
    [Display(Name = "Minimum Proficiency Level")]
    public int MinimumProficiencyLevel { get; set; } // Minimum proficiency level required (1-5)
    
    [Display(Name = "Priority")]
    public string Priority { get; set; } = "Medium"; // Low, Medium, High, Critical
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "Last Updated")]
    public DateTime? LastUpdated { get; set; }
}