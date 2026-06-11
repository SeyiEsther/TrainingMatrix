using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrainingMatrixApp.Models;

public class EmployeeSkill
{
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }
    
    [ValidateNever]
    public Employee Employee { get; set; } = null!;
    
    [Required]
    [Display(Name = "Skill")]
    public int SkillId { get; set; }
    
    [ValidateNever]
    public Skill Skill { get; set; } = null!;
    
    [Required]
    [Range(1, 5)]
    [Display(Name = "Proficiency Level")]
    public int ProficiencyLevel { get; set; } // 1-5 scale
    
    [Required]
    [Display(Name = "Acquired Date")]
    [DataType(DataType.Date)]
    public DateTime AcquiredDate { get; set; }
    
    [Display(Name = "Last Assessed")]
    [DataType(DataType.Date)]
    public DateTime? LastAssessedDate { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}